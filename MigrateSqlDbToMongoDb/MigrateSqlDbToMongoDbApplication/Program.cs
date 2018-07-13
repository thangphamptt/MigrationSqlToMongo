using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Services;
using MongoDatabaseHrToolv1.DbContext;
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

            Task.Run(async () =>
            {
                //await MigrateJob();
                //await MigrateOrganizationalUnit();
                await MigrateOffer();
            });
           // MigrateCandidate();
            Console.ReadKey();

        }
        static void MigrateCandidate()
        {
            HrToolv1DbContext hrToolDbContext = new HrToolv1DbContext(configuration);
            var candidates = hrToolDbContext.Candidates.ToList();
            var migrateCandidate = new MigrateCandidateToCandidateService();

            Console.WriteLine("Start migrate candidate to Services.....");

            var insertCandidateToCandidateService = migrateCandidate.InsertCandidateToCandidateService(configuration, candidates).Result;
            Console.WriteLine("{0} candidate(s) inserted to CandidateService", insertCandidateToCandidateService);

            var insertCandidateToInterviewService = migrateCandidate.InsertCandidateToInterviewService(configuration, candidates).Result;
            Console.WriteLine("{0} candidate(s) inserted to InterviewService", insertCandidateToInterviewService);

            var insertCandidateToJobService = migrateCandidate.InsertCandidateToJobService(configuration, candidates).Result;
            Console.WriteLine("{0} candidate(s) inserted to JobService", insertCandidateToJobService);

            var insertCandidateToJobMatchingService = migrateCandidate.InsertCandidateToJobMatchingService(configuration, candidates).Result;
            Console.WriteLine("{0} candidate(s) inserted to JobMatchingService", insertCandidateToJobMatchingService);

            var insertCandidateToOfferService = migrateCandidate.InsertCandidateToOfferService(configuration, candidates).Result;
            Console.WriteLine("{0} candidate(s) inserted to OfferService", insertCandidateToOfferService);

            var insertCandidateToScheduleService = migrateCandidate.InsertCandidateToScheduleService(configuration, candidates).Result;
            Console.WriteLine("{0} candidate(s) inserted to ScheduleService", insertCandidateToScheduleService);
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

        static async Task MigrateOffer()
        {
            Console.WriteLine("Start migrate [offer] to [Offer Service].....");
            var migrateOfferToOfferService = new MigrateOfferToOfferService();
            var offers = await migrateOfferToOfferService.Execute(configuration);
            Console.WriteLine("Migrate [offer] to [Offer Service] Succeed {0} records \n", offers);
        }
    }
}
