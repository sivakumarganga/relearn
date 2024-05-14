using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Relearn.UnitTests.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var hostBuilder=Host.CreateDefaultBuilder(args);
            //hostBuilder.ConfigureServices(services => {


            //});
            //hostBuilder.ConfigureLogging(logging => {
            //    logging.Services.AddLogging();
            //});
            //hostBuilder.ConfigureAppConfiguration((context, config) => {

            //});
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = factory.CreateLogger<SampleLogic>();

            SampleLogic sampleLogic = new SampleLogic(logger);
            sampleLogic.Run();
            Console.Read();
        }
    }
}
