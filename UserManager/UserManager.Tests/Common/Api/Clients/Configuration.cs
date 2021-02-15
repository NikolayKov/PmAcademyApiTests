using Microsoft.Extensions.Configuration;


namespace UserManager.Tests.Common.Api.Clients
{
    public class Configuration
    {
        private static IConfigurationRoot _instance;
        private static readonly object _syncRoot = new object();

        protected Configuration() { }

        public static IConfigurationRoot GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        var configBuilder = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", false, true);
                        _instance = configBuilder.Build();
                    }

                }
            }

            return _instance;
        }
    }
}
