using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.UnitTests.ConsoleApp
{
    public class SampleLogic
    {
        ILogger Logger;
        public SampleLogic(ILogger<SampleLogic> logger)
        {
            Logger = logger;
        }
        public void Run()
        {
            Logger.LogInformation("Hello, World!");
        }
    }
}
