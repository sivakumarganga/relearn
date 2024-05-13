using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using MSILogger = Microsoft.Extensions.Logging.ILogger;

namespace Relearn.DotNet.Logging
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            SimpleLogger();
            LoggerWithSerilog();

            Console.Read();
        }
        public static void SimpleLogger()
        {
            Console.WriteLine("--------------- MS Logger--------------");
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            MSILogger logger = factory.CreateLogger("Program");
            logger.LogInformation("Hello World! Logging is {Description}.", "fun");
            /*
             info: Program[0]
                Hello World! Logging is fun.

             */
        }
        public static void LoggerWithSerilog()
        {

            Console.WriteLine("--------------- SeriLogger--------------");
            Log.Logger = new LoggerConfiguration()
                           // add console as logging target
                           .WriteTo.Console(new JsonFormatter())
                           // set default minimum level
                           .Enrich.FromLogContext()
                           .MinimumLevel.Debug()
                           .CreateLogger();

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Logging.AddSerilog();
            var host = builder.Build();
            MSILogger logger = host.Services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Hello World! Logging is {Description}.", "fun");
            /*
            --------------- SeriLogger--------------
info: Relearn.DotNet.Logging.Program[0]
      Hello World! Logging is fun.
{"Timestamp":"2024-05-13T12:32:48.1934359+05:30","Level":"Information","MessageTemplate":"Hello World! Logging is {Description}.","Properties":{"Description":"fun","SourceContext":"Relearn.DotNet.Logging.Program"}}

             */

        }
    }
}
