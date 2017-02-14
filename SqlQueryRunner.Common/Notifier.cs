using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlQueryRunner.Common
{
    public class Notifier:IDisposable
    {
        public ContextMenuStrip cmnuNotifyIcon { get; private set; }
        public ToolStripMenuItem cmnuStop { get; private set; }
        public ToolStripMenuItem cmnuStart { get; private set; }
        public ToolStripSeparator toolStripMenuItem1 { get; private set; }
        public ToolStripMenuItem cmnuExit { get; private set; }
        public NotifyIcon notifyIcon { get; private set; }

        public NotifyIcon CreateNotifyIcon(string text)
        {
            cmnuNotifyIcon = new System.Windows.Forms.ContextMenuStrip();
            cmnuStop = new System.Windows.Forms.ToolStripMenuItem();
            cmnuStart = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            cmnuExit = new System.Windows.Forms.ToolStripMenuItem();

            cmnuNotifyIcon.ImageScalingSize = new System.Drawing.Size(20, 20);
            cmnuNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            cmnuStart,
            cmnuStop,
            toolStripMenuItem1,
            cmnuExit});
            cmnuNotifyIcon.Name = "cmnuNotifyIcon";
            cmnuNotifyIcon.Size = new System.Drawing.Size(116, 88);
            // 
            // cmnuStop
            // 
            cmnuStop.Name = "cmnuStop";
            cmnuStop.Size = new System.Drawing.Size(115, 26);
            cmnuStop.Text = "Stop";
            // 
            // cmnuStart
            // 
            cmnuStart.Name = "cmnuStart";
            cmnuStart.Size = new System.Drawing.Size(115, 26);
            cmnuStart.Text = "Start";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(112, 6);
            // 
            // cmnuExit
            // 
            cmnuExit.Name = "cmnuExit";
            cmnuExit.Size = new System.Drawing.Size(115, 26);
            cmnuExit.Text = "Exit";


            notifyIcon = new NotifyIcon();

            notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            notifyIcon.ContextMenuStrip = cmnuNotifyIcon;
            notifyIcon.Icon = Common.Properties.Resources.Visual_Studio_alt;
            notifyIcon.Text = text;
            notifyIcon.Visible = true;

            return notifyIcon;
        }

        public void Show(string tipText)
        {
            if (Options.showNotification)
            {
                this.notifyIcon.BalloonTipTitle = Options.notifyTipTitle;
                this.notifyIcon.BalloonTipText = tipText;
                this.notifyIcon.Text = this.notifyIcon.BalloonTipText;
                this.notifyIcon.ShowBalloonTip(1000);
            }
        }

        public void Dispose()
        {
            this.cmnuExit.Dispose();
            this.cmnuStart.Dispose();
            this.cmnuStop.Dispose();
            this.toolStripMenuItem1.Dispose();
            this.cmnuNotifyIcon.Dispose();
            this.notifyIcon.Dispose();
        }
    }
}
