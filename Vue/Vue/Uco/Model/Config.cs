using System.Web;
using System.Configuration;

namespace Vue
{
    public static class Config
    {
        public static string ConectionString
        {
            get {
                string ConnStr = string.Empty;
                var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"];
                if(connSetting != null)
                {
                    ConnStr = connSetting.ConnectionString;
                }
                return ConnStr;
            }
        }
    }
}