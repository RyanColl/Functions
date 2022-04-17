using System;
using Functions.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Functions.StartUp))]
namespace Functions
{
    public class StartUp : FunctionsStartup {
        public override void Configure(IFunctionsHostBuilder builder) {
            string connStr = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

            builder.Services.AddDbContext<ApplicationDbContext>(
              options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connStr));

        }
    }
}
