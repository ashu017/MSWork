using Grpc.Net.Client;
using GrpcServiceSample;
using System;
using System.Collections.Generic;
using System.Reflection;
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

        private static void Temp()
        {
            List<Type> type = new List<Type>();
            SortedSet<string> te = new SortedSet<string>();
            SortedDictionary<string, string> sor = new SortedDictionary<string, string>();
            /* foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsSubclassOf(typeof(AcisSMESystemMetadataReadOnlyOperation)) ||
                    t.IsSubclassOf(typeof(AcisSMESystemMetadataReadWriteOperation)))
                {
                    type.Add(t);
                    var tem = Activator.CreateInstance(t);
                    var ty = tem.GetType().GetProperty("OperationGroup").GetValue(tem);
                    var st = ty.GetType().GetProperty("Name").GetValue(ty).ToString();
                    sor.Add(t.ToString(), st);
                    te.Add(st);
                }
            } */

            Assembly a = Assembly.Load("Microsoft.Azure.UsageBilling.GenevaExtensions");
            foreach (Type t in a.GetTypes())
            {
                if (t.IsSubclassOf(typeof(AcisSMEOperation)))
                {
                    type.Add(t);
                    var tem = Activator.CreateInstance(t);
                    var ty = tem.GetType().GetProperty("OperationGroup").GetValue(tem);
                    var st = ty.GetType().GetProperty("Name").GetValue(ty).ToString();
                    sor.Add(t.ToString(), st);
                    te.Add(st);
                }
            }

            /* var temp = new List<AssemblyName>();
            foreach (AssemblyName assembly in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                temp.Add(assembly);
            }

            var li = new List<string>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.Contains("UsageBilling"))
                {
                    IList<Type> typesInCurrentAssembly = assembly.GetTypes();
                    foreach (Type t in assembly.GetTypes())
                    {
                        if (t.IsSubclassOf(typeof(AcisSMESystemMetadataReadOnlyOperation)) ||
                    t.IsSubclassOf(typeof(AcisSMESystemMetadataReadWriteOperation)))
                        {
                            type.Add(t);
                            var tem = Activator.CreateInstance(t);
                            var ty = tem.GetType().GetProperty("OperationGroup").GetValue(tem);
                            var st = ty.GetType().GetProperty("Name").GetValue(ty).ToString();
                            sor.Add(t.ToString(), st);
                            te.Add(st);
                        }
                    }
                }
            } */
        }
    }
}
