using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Services;
using SqlDatabase.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication
{
    class Program
    {
        private static IConfiguration configuration;

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
            var transfer = new MigrateCandidate();
            Task.Run(async () =>
            {
                var candidate = await transfer.Execute(configuration);
                Console.WriteLine("Insert successed {0} ", candidate);
            });           
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
