using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlQueryRunner.Common
{
    public class ReportObj
    {
        public int ResultCount { get; set; }
        public DataTable ResultTable { get; set; }

        public void Report()
        {
            Thread t = new Thread(new ParameterizedThreadStart((repObj) =>
            {
                ReportObj rObj = repObj as ReportObj;
                string tableStr = string.Empty;
                string countStr = rObj.ResultCount.ToString();

                if (Options.resultType == "table")
                {
                    StringBuilder sb = new StringBuilder("<table border='1'>");
                    DataTable table = rObj.ResultTable as DataTable;

                    foreach (DataRow dr in table.Rows)
                    {
                        sb.AppendLine("<tr>");

                        foreach (DataColumn dc in table.Columns)
                        {
                            sb.AppendLine($"<td>{dr.Field<string>(dc)}</td>");
                        }

                        sb.AppendLine("</tr>");
                    }

                    sb.AppendLine("</table>");
                    tableStr = sb.ToString();
                }

                string mailBody = Options.mailBody.Replace(
                    "{datetime}", DateTime.Now.ToString()).Replace(
                    "{count}", countStr).Replace(
                    "{table}", tableStr).Replace(
                    "{br}", "<br />").Replace(
                    "{hr}", "<hr />");

                MailHelper.SendMail(mailBody, Options.mailTo, Options.subject, true);
            }));

            t.Start(this);
        }
    }
}
