using System.Reflection;
using Microsoft.Extensions.Configuration;
namespace Relearn.DotNet.Configs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// Add configuration to the configbuilder in order, last one overrides the first ones
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json")
                      .AddJsonFile("appsettings.Development.json", true)
                      .AddUserSecrets(Assembly.GetEntryAssembly()!)
                      .AddEnvironmentVariables();
            var configuration = configBuilder.Build();

            ///
            Console.WriteLine($"Hello, World! {configuration["FirstSecret"]}");
        }
    }
}
