using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using Spg.ProbeFachtheorie.Aufgabe2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SeedService seedService = new SeedService(
                new LibraryContext(
                    new DbContextOptionsBuilder().UseSqlite("Data Source=Library.db")
                .Options));
            seedService.DeleteDatabase();
            seedService.CreateDatabase();
            seedService.Seed();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
