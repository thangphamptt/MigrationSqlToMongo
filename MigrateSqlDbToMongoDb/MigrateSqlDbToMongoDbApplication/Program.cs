using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Services;
using System;
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

            var transfer = new MigrateJob();
            Task.Run(async () =>
            {
                var jobs = await transfer.Execute(configuration);
                Console.WriteLine("Insert successed {0} {1}", jobs, nameof(jobs));
            });           
            Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
        }
    }
}
