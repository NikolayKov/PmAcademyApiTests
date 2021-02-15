using WireMock.Server;

namespace UserManager.Tests.Common.Api.Mock
{
    public class MockServer
    {
        public static WireMockServer Start()
        {
            return WireMockServer.Start(7777);
        }
    }
}
