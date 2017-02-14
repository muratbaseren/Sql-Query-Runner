using SqlQueryRunner.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlQueryRunner.Winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool isPlaying = false;

        public int Count { get; set; } = 0;
        public bool IsPlaying
        {
            get
            {
                return isPlaying;
            }
            set
            {
                isPlaying = value;

                timer1.Enabled = isPlaying;
                NotifierObj.cmnuStart.Enabled = !isPlaying;
                NotifierObj.cmnuStop.Enabled = isPlaying;

                if (isPlaying)
                {
                    btnPlayStop.Image = SqlQueryRunner.Common.Properties.Resources._1486899825_stop;
                    btnPlayStop.Text = "Stop";
                }
                else
                {
                    btnPlayStop.Image = SqlQueryRunner.Common.Properties.Resources._1486899589_play_circle_outline;
                    btnPlayStop.Text = "Play";
                }
            }
        }

        private DataAccessObj DataAccess = new DataAccessObj();
        private Notifier NotifierObj = new Notifier();


        private void timer1_Tick(object sender, EventArgs e)
        {
            // Do work with new thread..
            Thread mt = new Thread(new ThreadStart(() =>
            {
                try
                {
                    #region Run SQL Query..
                    // Run SQL Query..
                    DataTable dt = new DataTable();
                    Count += DataAccess.Adapter.Fill(dt);

                    if (label1.InvokeRequired)
                    {
                        label1.Invoke(new Action(() =>
                        {
                            label1.Text = Options.labelPrefix + Count;
                        }));
                    }
                    #endregion


                    // Show Notification..
                    NotifierObj.Show(string.Format(Options.notifyTipText, Count));

                    // Send mail with new thread.
                    if (Options.sendMail)
                    {
                        ReportObj reportObj = new ReportObj();
                        reportObj.ResultCount = Count;
                        reportObj.ResultTable = dt;

                        reportObj.Report();
                    }
                }
                catch (Exception ex)
                {
                    if (label2.InvokeRequired)
                    {
                        label2.Invoke(new Action(() =>
                        {
                            label2.Text = ex.Message;
                            IsPlaying = false;
                        }));
                    }
                }

            }));

            mt.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Options.appTitle;
            timer1.Interval = Options.interval;
            Visible = Options.visible;

            label1.Text = Options.labelPrefix + Count;
            label2.Text = string.Empty;

            NotifierObj.CreateNotifyIcon(Options.notifyTipTitle);
            NotifierObj.cmnuStart.Click += cmnuStart_Click;
            NotifierObj.cmnuStop.Click += cmnuStop_Click;
            NotifierObj.cmnuExit.Click += cmnuExit_Click;

            IsPlaying = true;
        }

        private void btnPlayStop_Click(object sender, EventArgs e)
        {
            IsPlaying = !IsPlaying;
        }

        private void cmnuStart_Click(object sender, EventArgs e)
        {
            IsPlaying = true;
        }

        private void cmnuStop_Click(object sender, EventArgs e)
        {
            IsPlaying = false;
        }

        private void cmnuExit_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();

            DataAccess.Dispose();
            NotifierObj.Dispose();

            Application.ExitThread();
        }
    }
}
