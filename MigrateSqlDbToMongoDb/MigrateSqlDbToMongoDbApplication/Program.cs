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

            Task.Run(async () =>
            {
                await MigrateJob();
                await MigrateOrganizationalUnit();
            });           
            Console.ReadKey();
        }

        static async Task MigrateJob()
        {
            Console.WriteLine("Start migrate job to Job Service.....");
            var migrateJobToJobService = new MigrateJobToJobService();
            var jobs = await migrateJobToJobService.Execute(configuration);
            Console.WriteLine("Migrate job to Job Service Succeed {0} records \n", jobs);

            Console.WriteLine("Start migrate job to Candidate Service.....");
            var migrateJobToCandidateService = new MigrateJobToCandidateService();
            jobs = await migrateJobToCandidateService.Execute(configuration);
            Console.WriteLine("Migrate job to Candidate Service Succeed {0} records \n", jobs);

            Console.WriteLine("Start migrate job to Interview Service.....");
            var migrateJobToInterviewService = new MigrateJobToInterviewService();
            jobs = await migrateJobToInterviewService.Execute(configuration);
            Console.WriteLine("Migrate job to Interview Service Succeed {0} records \n", jobs);

            Console.WriteLine("Start migrate job to Offer Service.....");
            var migrateJobToOfferService = new MigrateJobToOfferService();
            jobs = await migrateJobToOfferService.Execute(configuration);
            Console.WriteLine("Migrate job to Offer Service Succeed {0} records \n", jobs);
        }

        static async Task MigrateOrganizationalUnit()
        {
            var companyId = configuration.GetSection("CompanySetting:Id").Value;
            var companyName = configuration.GetSection("CompanySetting:Name").Value;
            var organizationalUnitService = new OrganizationalUnitService.OrganizationalUnitService(configuration);
            var organizationalUnits = await organizationalUnitService.AddOrganizationalUnit(companyId, companyName);
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
                Console.WriteLine("OrganizationalUnit existed.\n");
            }
        }
    }
}
