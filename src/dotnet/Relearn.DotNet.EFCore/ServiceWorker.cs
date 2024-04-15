using Microsoft.Extensions.Hosting;
using Relearn.DotNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.EFCore
{
    internal class AppServiceWorker: IHostedService
    {
        public RelearnDbContext _dbContext { get; set; }
        public AppServiceWorker(RelearnDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service started");
            var userRole = _dbContext.Set<UserRole>().Where(_=>_.User.Username == "admin").FirstOrDefault();
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service stopped");
            await Task.CompletedTask;
        }
    }
}
