﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SchoolProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())

            {

                var services = scope.ServiceProvider;

                try

                {

                    var serviceProvider = services.GetRequiredService<IServiceProvider>();

                    var configuration = services.GetRequiredService<IConfiguration>();

                }

                catch (Exception exception)

                {

                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(exception, "An error occurred while creating roles");

                }

            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
