using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ETT.Logic.Interfaces;
using ETT.Storage.Context;
using ETT.Web.Models.Users;
using ETT.Web.Util.Initializers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ETT.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                //Task task;
                List<Task> tasks = new List<Task>();
                var services = scope.ServiceProvider;
                try
                {
                    // app context
                    //ApplicationContext appContext = new ApplicationContext();

                    // getting services
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<Role>>();
                    //var projectService = services.GetRequiredService<IProjectService>();

                    // adding administrator
                    tasks.Add(UserRolesInitializer.InitializeAsync(userManager, rolesManager));

                    //other initializers

                    Task.WaitAll(tasks.ToArray());
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
