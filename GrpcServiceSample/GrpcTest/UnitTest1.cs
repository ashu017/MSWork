using Grpc.Net.Client;
using GrpcServiceSample;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using static GrpcServiceSample.Greeter;

namespace GrpcTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
    }

    [TestClass]
    public class BasicTests
    {

        private readonly WebApplicationFactory<Startup> _factory = new WebApplicationFactory<Startup>();
        [TestMethod]
        public async Task greetertestAsync()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices
                            .GetRequiredService<ApplicationDbContext>();
                        var logger = scopedServices
                            .GetRequiredService<ILogger<IndexPageTests>>();

                        try
                        {
                            Utilities.ReinitializeDbForTests(db);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding " +
                                "the database with test messages. Error: {Message}",
                                ex.Message);
                        }
                    }
                });
            })
        .CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

            // Act
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var _client = new GreeterClient(channel);
            var request = new HelloRequest
            {
                Name = "Ashutosh"
            };
            var response = await _client.SayHelloAsync(request);

            // Assert
            //response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert.Equals("text/html; charset=utf-8",
            //    response.Content.Headers.ContentType.ToString());
        }
    }
}

