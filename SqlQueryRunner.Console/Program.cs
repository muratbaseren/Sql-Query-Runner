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

namespace SqlQueryRunner.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Console_Load();

            //string num = "";
            //string msg = "";

            //do
            //{
            //    System.Console.WriteLine(msg);
            //    p.WriteMenu();
            //    System.Console.Write("Your selection => ");
            //    string t = System.Console.ReadLine();

            //} while (p.CheckAvailability(num, out msg) != 0);

            System.Console.ReadKey();
        }


        private bool isPlaying = false;
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private DataAccessObj DataAccess = new DataAccessObj();
        private Notifier NotifierObj = new Notifier();

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
            }
        }

        public void WriteMenu()
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine();
            System.Console.WriteLine("=================");
            System.Console.WriteLine("       MENÜ      ");
            System.Console.WriteLine("=================");
            System.Console.WriteLine(" [1] - Start");
            System.Console.WriteLine(" [2] - Stop");
            System.Console.WriteLine(" [0] - Exit App");
            System.Console.WriteLine();
            System.Console.ResetColor();
        }
        public int CheckAvailability(string num, out string message)
        {
            int res = -1;
            message = "";

            if (int.TryParse(num, out res))
            {
                if (res > -1 && res < 3)
                {
                    if (res != 0)
                    {
                        if (isPlaying && res == 1)
                        {
                            message = "Already process is started.";
                        }
                        else if (isPlaying && res == 2)
                        {
                            IsPlaying = false;
                            message = "Process is stopped.";
                        }
                        else if (!isPlaying && res == 1)
                        {
                            IsPlaying = true;
                            message = "Process is started.";
                        }
                        else if (!isPlaying && res == 2)
                        {
                            message = "Already process is stopped.";
                        }
                    }
                    else
                    {
                        message = "Press any key for exit application.";
                    }
                }
                else
                {
                    message = "incorrect menu selection. Please try again";
                    res = -1;
                }
            }
            else
            {
                message = "incorrect menu selection. Please try again";
            }

            return res;
        }




        public void Console_Load()
        {
            System.Console.Title = Options.appTitle;

            NotifierObj.CreateNotifyIcon(Options.notifyTipTitle);
            NotifierObj.cmnuStart.Click += cmnuStart_Click;
            NotifierObj.cmnuStop.Click += cmnuStop_Click;
            NotifierObj.cmnuExit.Click += cmnuExit_Click;

            timer1.Tick += timer1_Tick;
            timer1.Interval = Options.interval;

            IsPlaying = true;
        }

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
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(ex.Message);
                    System.Console.ResetColor();

                    IsPlaying = false;
                }

            }));

            mt.Start();
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
