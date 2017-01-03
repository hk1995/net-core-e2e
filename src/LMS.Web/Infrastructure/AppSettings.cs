using Microsoft.Extensions.Configuration;

namespace LMS.Web.Infrastructure
{
    public static class AppSettings
    {
        public static string ConnectionStringName { get; private set; }
        public static string AppRoot { get; private set; }
        public static string Auth0Domain { get; private set; }
        public static string Auth0NewUserKey { get; private set; }
        public static string StorageConnectionString { get; private set; }
        public static string CdnEndpoint { get; private set; }
        public static string NoImageUrl { get; set; }

        public static void SetEnviornmentalSettings(IConfigurationRoot configuration)
        {
            ConnectionStringName = "Default";
            AppRoot = configuration["General:AppAllowOrigin"];
            Auth0NewUserKey = configuration["Auth0:NewUserKey"];
            Auth0Domain = configuration["Auth0:Domain"];
            StorageConnectionString = configuration["Azure:StorageConnectionString"];
            CdnEndpoint = configuration["Azure:CdnEndpoint"];
            NoImageUrl = configuration["Azure:NoImageUrl"];
        }
    }
}
