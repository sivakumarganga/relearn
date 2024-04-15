using Microsoft.EntityFrameworkCore;
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
            var userProfile= _dbContext.Set<UserProfile>().Where(_=>_.User.Username == "admin").FirstOrDefault();
            var userProfileWithUser= _dbContext.Set<UserProfile>().Include(_=>_.User).Where(_=>_.User.Username == "admin").FirstOrDefault();
            var userProfileWithUserNavigation= _dbContext.Set<UserProfile>().Include("User").Where(_=>_.User.Username == "admin").FirstOrDefault();
            var userProfileWithUserRole= _dbContext.Set<UserProfile>().Include(_=>_.User.UserRoles).Where(_=>_.User.Username == "admin").FirstOrDefault();
            var userRolesByeProfileId= _dbContext.Set<UserProfile>().Where(_=>_.UserId==10).Join(_dbContext.Set<UserRole>().Where(_=>_.RoleId==11), _=>_.UserId, _=>_.UserId, (profile, role)=> new {profile, role}).ToList();
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service stopped");
            await Task.CompletedTask;
        }
    }
}
