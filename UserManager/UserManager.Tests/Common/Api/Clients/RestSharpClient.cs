using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace UserManager.Tests.Common.Api.Clients
{
    public class RestSharpClient
    {
        private static RestClient _instance;
        private static readonly object _syncRoot = new object();

        protected RestSharpClient() { }

        public static RestClient GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
                        var externalApi = configBuilder.GetSection("EventsManagerApi").Value;
                        _instance = new RestClient(externalApi);
                        return _instance;
                    }
                }
            }

            return _instance;
        }
    }
}
