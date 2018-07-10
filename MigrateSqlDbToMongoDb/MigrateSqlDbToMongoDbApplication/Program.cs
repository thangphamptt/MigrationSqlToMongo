using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlDatabase.Model;
using System;
using System.Linq;

namespace MigrateSqlDbToMongoDbApplication
{
    class Program
    {
        private static IConfigurationRoot configuration;

        private static void Configuration()
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();              
        }       

        static void Main(string[] args)
        {
            Configuration();

            var sqlConnectionString = configuration.GetSection("SQLDB:ConnectionString").Value;
            using (var dbContext = HrToolDbContextFactory.CreateDbContext(sqlConnectionString))
            {
                var data = dbContext.Template.ToList();
                Console.WriteLine("Candidate count: {0}", data.Count);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
