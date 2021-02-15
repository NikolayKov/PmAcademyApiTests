using System;
using System.Collections.Generic;
using System.Text;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace UserManager.Tests.Common.Api.Mock
{
    public class Mock
    {
        public static void Get(string path, object fakeResponse)
        {
            var server = MockServer.Start();

            server
                .Given(
                    Request.Create().WithPath(path).UsingGet()
                )
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json")
                        .WithBodyAsJson(fakeResponse)
                );
        }
    }
}
