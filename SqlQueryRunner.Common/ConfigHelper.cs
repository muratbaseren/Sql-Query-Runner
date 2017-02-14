using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryRunner.Common
{
    public class ConfigHelper
    {
        public static string GetAS(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
                return ConfigurationManager.AppSettings[key];
            else
                return null;
        }

        public static T GetAS<T>(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            else
                return default(T);
        }

        public static string GetCS(string name)
        {
            if (ConfigurationManager.ConnectionStrings[name] != null)
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            else
                return null;
        }
    }
}
