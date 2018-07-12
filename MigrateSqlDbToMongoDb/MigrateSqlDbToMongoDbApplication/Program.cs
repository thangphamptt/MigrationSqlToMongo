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
            var companyId = configuration.GetSection("CompanySetting:Id").Value;
            var companyName = configuration.GetSection("CompanySetting:Name").Value;
            var transfer = new MigrateJob();

            Task.Run(async () =>
            {
                var jobs = await transfer.Execute(configuration);
                Console.WriteLine("Insert successed {0} {1}", jobs, nameof(jobs));
            });

            var organizationalUnitService = new MigrateSqlDbToMongoDbApplication.OrganizationalUnitService.OrganizationalUnitService(configuration);
            var organizationalUnits = organizationalUnitService.AddOrganizationalUnit(companyId, companyName).Result;
            if (organizationalUnits.Count != 0)
            {
                Console.WriteLine("{0} OrganizationalUnit(s) added succesfully.", organizationalUnits.Count);
                foreach (var item in organizationalUnits)
                {
                    Console.WriteLine("-------{0}-------", item);
                }
            }
            else
            {
                Console.WriteLine("OrganizationalUnit existed.");
            }
            Console.ReadKey();
        }
    }
}
