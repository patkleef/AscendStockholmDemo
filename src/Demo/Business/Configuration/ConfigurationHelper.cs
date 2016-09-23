using System.Configuration;

namespace Demo.Business.Configuration
{
    public static class ConfigurationHelper
    {
        public static string GoogleClientId
        {
            get { return ConfigurationManager.AppSettings["GoogleClientId"]; }
        }

        public static string GoogleClientSecret
        {
            get { return ConfigurationManager.AppSettings["GoogleClientSecret"]; }
        }

        public static string ApplicationName
        {
            get { return "People API .NET Quickstart"; }
        }
    }
}