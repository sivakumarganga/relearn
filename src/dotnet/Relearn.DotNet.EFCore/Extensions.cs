using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Relearn.DotNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.EFCore
{
    public static class Extensions
    {
        public static async Task ApplyMigrationsAsync(this IHost app)
        {
            Console.WriteLine("Applying Migrations");
            await using var scope = app.Services.CreateAsyncScope();
            var context = scope.ServiceProvider.GetService<RelearnDbContext>();

            if (context is null)
                throw new Exception("Database Context Not Found");

            await context.Database.MigrateAsync();
            Console.WriteLine("Applying Migrations completed");
        }
        /// <summary>
        /// Dynamicaly register all Entities that inherit from specific BaseType
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="baseType">Base type that Entities inherit from this</param>
        /// <param name="assemblies">Assemblies contains Entities</param>
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
                modelBuilder.Entity(type);
        }
        public static async Task SeedDefaultDataAsync(this IHost app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetService<RelearnDbContext>();
            if (dbContext is null)
                return;
            Console.WriteLine("Seeding Default Data");
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var dbSetUsers = dbContext.Set<User>();
                    var dbSetRoles = dbContext.Set<Role>();
                    var dbSetUserRoles = dbContext.Set<UserRole>();

                    List<User> users = new List<User>() {
                            new ()
                                {
                                    Username = "admin",
                                    Password = "admin",
                                    Email = "admin@anysite.com"
                                },
                            new ()
                            {
                                    Username = "employee",
                                    Password = "employee",
                                    Email = "employee@anysite.com"
                            },
                            new ()
                            {
                                    Username = "guest",
                                      Password = "guest",
                                    Email = "guest@anysite.com"
                            }
                    };
                    foreach (var user in users)
                    {
                        if (!await dbSetUsers.AnyAsync(_ => _.Username == user.Username))
                        {
                            await dbSetUsers.AddAsync(user);
                        }
                    }

                    List<Role> roles = new List<Role>()
                            {
                                new() {
                                    Name = "Admin",
                                    Description = "Admin Role"
                                },
                                new ()
                                {
                                    Name = "Employee",
                                    Description = "Employee Role"
                                },
                                new ()
                                {
                                    Name = "Guest",
                                    Description = "Guest Role"
                                }
                             };
                    foreach (var role in roles)
                    {
                        if (!await dbSetRoles.AnyAsync(_ => _.Name == role.Name))
                        {
                            await dbSetRoles.AddAsync(role);
                        }
                    }
                    await dbContext.SaveChangesAsync();
                    // add user roles
                    List<UserRole> userRoles = new List<UserRole>()
                        {
                            new ()
                            {
                                UserId = users[0].Id,
                                RoleId = roles[0].Id
                            },
                            new ()
                            {
                                UserId = users[1].Id,
                                RoleId = roles[1].Id
                            },
                            new ()
                            {
                                UserId = users[2].Id,
                                RoleId = roles[2].Id
                            }
                        };

                    foreach (var userRole in userRoles)
                    {
                        if (!await dbSetUserRoles.AnyAsync(_ => _.UserId == userRole.UserId && _.RoleId == userRole.RoleId))
                        {
                            await dbSetUserRoles.AddAsync(userRole);
                        }
                    }
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Seeding Default Data Completed");
        }
    }
}
