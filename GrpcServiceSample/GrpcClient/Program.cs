using Grpc.Net.Client;
using GrpcServiceSample;
using System;
using System.Threading.Tasks;
using static GrpcServiceSample.Greeter;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new GreeterClient(channel);
            var _client = new GreeterClient(channel);
            var request = new HelloRequest
            {
                Name = "Ashutosh",
                Phone = "123"
            };
            var response = await client.SayHelloAsync(request);
            Console.WriteLine(response.Message);
        }
    }
}
