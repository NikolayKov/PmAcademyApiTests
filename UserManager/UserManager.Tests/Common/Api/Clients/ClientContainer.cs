using System;
using System.Collections.Generic;
using System.Text;

namespace UserManager.Tests.Common.Api.Clients
{
    public class ClientContainer
    {
        private static readonly object _syncRoot = new object();
        private static ClientContainer _instance;

        protected ClientContainer()
        {
        }

        public static ClientContainer GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new ClientContainer();
                }
            }
            return _instance;
        }
    }
}
