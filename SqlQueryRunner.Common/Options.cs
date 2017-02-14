using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryRunner.Common
{
    public static class Options
    {
        public static string connstr = ConfigHelper.GetCS("DefaultConnection");

        public static string labelPrefix = ConfigHelper.GetAS("labelTextPrefix");
        public static string query = ConfigHelper.GetAS("query");
        public static bool showNotification = ConfigHelper.GetAS<bool>("showNotification");
        public static string notifyTipTitle = ConfigHelper.GetAS("notifyTipTitle");
        public static string notifyTipText = ConfigHelper.GetAS("notifyTipText");
        public static string resultType = ConfigHelper.GetAS("resultType");

        public static bool sendMail = ConfigHelper.GetAS<bool>("sendMail");
        public static string mailTo = ConfigHelper.GetAS("mailTo");
        public static string mailBody = ConfigHelper.GetAS("mailBody");
        public static string subject = ConfigHelper.GetAS("subject");
        public static string mailServer = ConfigHelper.GetAS("mailServer");
        public static int mailPort = ConfigHelper.GetAS<int>("mailPort");
        public static string mailFrom = ConfigHelper.GetAS("mailFrom");
        public static string mailPassword = ConfigHelper.GetAS("mailPassword");


        public static string appTitle = ConfigHelper.GetAS("appTitle");
        public static int interval = ConfigHelper.GetAS<int>("interval");
        public static bool visible = ConfigHelper.GetAS<bool>("visible");
    }
}
