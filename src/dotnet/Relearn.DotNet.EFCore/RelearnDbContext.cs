using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.EFCore
{
    public class RelearnDbContext : DbContext
    {
        public RelearnDbContext(DbContextOptions<RelearnDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RelearnDbContext).Assembly);
           // modelBuilder.AddRestrictDeleteBehaviorConvention();
            //modelBuilder.AddPluralizingTableNameConvention();
        }
    }
}
