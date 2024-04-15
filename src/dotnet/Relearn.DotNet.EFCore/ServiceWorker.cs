using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.EFCore
{
    internal class AppServiceWorker: IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service started");
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service stopped");
            await Task.CompletedTask;
        }
    }
}
