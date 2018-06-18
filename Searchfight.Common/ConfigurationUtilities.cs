using System.Configuration;
using System.Linq;

namespace Searchfight.Common
{
    public class ConfigurationUtilities
    {
        public static string GetConnectionStrings(string key)
        {            
            return ConfigurationManager.ConnectionStrings[key] != null
                     ? ConfigurationManager.ConnectionStrings[key].ConnectionString : string.Empty;
        }
        public static string GetAppSettings(string key, string defaultValue = "")
        {
            return ConfigurationManager.AppSettings.AllKeys.Any(k => k.Contains(key)) &&
                     ConfigurationManager.AppSettings[key] != null
                     ? ConfigurationManager.AppSettings[key].ToString() : defaultValue;
        }
        public static T GetAppSettings<T>(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Any(k => k.Contains(key)) &&
                     ConfigurationManager.AppSettings[key] != null &&
                     ConfigurationManager.AppSettings[key].TryParse<T>()
                     ? ConfigurationManager.AppSettings[key].Parse<T>() : default(T);
        }
        public static T GetAppSettings<T>(string key, T defaultValue)
        {
            return ConfigurationManager.AppSettings.AllKeys.Any(k => k.Contains(key)) &&
                     ConfigurationManager.AppSettings[key] != null &&
                     ConfigurationManager.AppSettings[key].TryParse<T>()
                     ? ConfigurationManager.AppSettings[key].Parse<T>() : defaultValue;
        }

        public static string MSNUrl => GetAppSettings("MSNUrl");
        public static string MSNKey => GetAppSettings("MSNKey");
        public static string GoogleUrl => GetAppSettings("GoogleUrl");
        public static string GoogleKey => GetAppSettings("GoogleKey");
        public static string GoogleCXKey => GetAppSettings("GoogleCXKey");
    }
}