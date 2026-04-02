using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SAP_Integration.Core.Configurations
{
    public static class AppSetting
    {
        public static string SapServer => ConfigurationManager.AppSettings["SapServer"];
        public static string SapServerSL => ConfigurationManager.AppSettings["SapServerSL"];

        public static string SapLicenseServer => ConfigurationManager.AppSettings["SapLicenseServer"];
        public static string SapCompanyDB => ConfigurationManager.AppSettings["SapCompanyDB"];
        public static string SapUserName => ConfigurationManager.AppSettings["SapUserName"];
        public static string SapPassword => ConfigurationManager.AppSettings["SapPassword"];

        public static bool UseServiceLayer
        {
            get
            {
            
                string configValue = ConfigurationManager.AppSettings["UseServiceLayer"];
                if (bool.TryParse(configValue, out bool result))
                {
                    return result;
                }

                return false;
            }
        }
    }
}
