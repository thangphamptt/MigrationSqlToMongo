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
            
            MigrateOrganizationalUnit();
            MigrateCandidate();
            MigrateApplication();
            MigrateInterview();
            MigrateSchedule();

            MigrateJob();
            MigrateOffer();
            MigrateTemplate();
            MigrateEmail();
            Console.ReadKey();
        }

        static void MigrateCandidate()
        {
            HrToolv1DbContext hrToolDbContext = new HrToolv1DbContext(configuration);
            var candidates = hrToolDbContext.Candidates.ToList();
            var migrateCandidate = new MigrateCandidateToCandidateService();

            Task.Run(async () =>
            {
                Console.WriteLine("========================================================= \n");
                Console.WriteLine("Migrate [candidate] to [Candidate Services] => Starting...");
                var insertCandidateToCandidateService = await migrateCandidate.InsertCandidateToCandidateService(configuration, candidates);
                Console.WriteLine($"Migrate [candidate] to [Candidate Services] => DONE: inserted {insertCandidateToCandidateService} Candidates.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [candidate] to [Interview Services] => Starting...");
                var insertCandidateToInterviewService = await migrateCandidate.InsertCandidateToInterviewService(configuration, candidates);
                Console.WriteLine($"Migrate [candidate] to [Interview Services] => DONE: inserted {insertCandidateToInterviewService} candidates.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [candidate] to [Job Services] => Starting...");
                var insertCandidateToJobService = await migrateCandidate.InsertCandidateToJobService(configuration, candidates);
                Console.WriteLine($"Migrate [candidate] to [Job Services] => DONE: inserted {insertCandidateToJobService} candidates.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [candidate] to [Job Matching Services] => Starting...");
                var insertCandidateToJobMatchingService = await migrateCandidate.InsertCandidateToJobMatchingService(configuration, candidates);
                Console.WriteLine($"Migrate [candidate] to [Job Matching Services] => DONE: inserted {insertCandidateToJobMatchingService} candidates.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [candidate] to [Offer Services] => Starting...");
                var insertCandidateToOfferService = await migrateCandidate.InsertCandidateToOfferService(configuration, candidates);
                Console.WriteLine($"Migrate [candidate] to [Offer Services] => DONE: inserted {insertCandidateToOfferService} candidates.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [candidate] to [Schedule Services] => Starting...");
                var insertCandidateToScheduleService = await migrateCandidate.InsertCandidateToScheduleService(configuration, candidates);
                Console.WriteLine($"Migrate [candidate] to [Schedule Services] => DONE: inserted {insertCandidateToScheduleService} candidates.\n");
            });
        }

        static void MigrateApplication()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("========================================================\n");
                Console.WriteLine("Migrate [application] to [Candidate Services] => Starting...");
                var applicationService = new MigrationApplicationToApplicationService(configuration);
                var totalApplicationsToCandidtate = await applicationService.ExecuteAsync();
                Console.WriteLine($"Migrate [application] to [Candidate Services] => DONE: inserted {totalApplicationsToCandidtate} applications.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("========================================================\n");
                Console.WriteLine("Migrate [application] to [Interview Services] => Starting...");
                var interviewService = new MigrationApplicationToInterviewService(configuration);
                var totalApplicationsToInterview = await interviewService.ExecuteAsync();
                Console.WriteLine($"Migrate [application] to [Interview Services] => DONE: inserted {totalApplicationsToInterview} applications.\n");
            });
        }

        static void MigrateJob()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("========================================================\n");
                Console.WriteLine("Migrate [job] to [Job Service] => Starting.....");
                var migrateJobToJobService = new MigrateJobToJobService();
                var jobs = await migrateJobToJobService.Execute(configuration);
                Console.WriteLine($"Migrate [job] to [Job Service] => DONE: inserted {jobs} jobs.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [job] to [Candidate Service] => Starting.....");
                var migrateJobToCandidateService = new MigrateJobToCandidateService();
                var jobs = await migrateJobToCandidateService.Execute(configuration);
                Console.WriteLine($"Migrate [job] to [Candidate Service] => DONE: inserted {jobs} jobs.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [job] to [Interview Service] => Starting.....");
                var migrateJobToInterviewService = new MigrateJobToInterviewService();
                var jobs = await migrateJobToInterviewService.Execute(configuration);
                Console.WriteLine($"Migrate [job] to [Interview Service] => DONE: inserted {jobs} jobs.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [job] to [Offer Service] => Starting.....");
                var migrateJobToOfferService = new MigrateJobToOfferService();
                var jobs = await migrateJobToOfferService.Execute(configuration);
                Console.WriteLine($"Migrate [job] to [Offer Service] => DONE: inserted {jobs} jobs.\n");
            });
        }

        static void MigrateOrganizationalUnit()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("=====================================================");
                Console.WriteLine("Migrate [organizationalUnit] => Starting...");
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
            });
        }

        static void MigrateOffer()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("=====================================================");
                Console.WriteLine("Migrate [offer] to [Offer Service] => Starting.....");
                var migrateOfferToOfferService = new MigrateOfferToOfferService();
                var offers = await migrateOfferToOfferService.Execute(configuration);
                Console.WriteLine($"Migrate [offer] to [Offer Service] => DONE: inserted {offers} offers.\n");
            });
        }

        static void MigrateTemplate()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("========================================================\n");
                Console.WriteLine("Migrate [template] to [Template Service] => Starting.....");
                var migrateTemplateToTemplateService = new MigrateTemplateToTemplateService();
                var templates = await migrateTemplateToTemplateService.Execute(configuration);
                Console.WriteLine($"Migrate [template] to [Template Service] => DONE: inserted {templates} jobs.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [template] to [Candidate Service] => Starting.....");
                var migrateTemplateToCandidateService = new MigrateTemplateToCandidateService();
                var templatesCandidate = await migrateTemplateToCandidateService.Execute(configuration);
                Console.WriteLine($"Migrate [template] to [Candidate Service] => DONE: inserted {templatesCandidate} jobs.\n");
            });

            Task.Run(async () =>
            {
                Console.WriteLine("Migrate [template] to [Interview Service] => Starting.....");
                var migrateTemplateToInterviewService = new MigrateTemplateToInterviewService();
                var templatesInterview = await migrateTemplateToInterviewService.Execute(configuration);
                Console.WriteLine($"Migrate [template] to [Interview Service] => DONE: inserted {templatesInterview} jobs.\n");
            });
        }

        static void MigrateEmail()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("========================================================\n");
                Console.WriteLine("Migrate [Email] to [Email Service] => Starting...");
                var migrateEmailService = new MigrationEmailToEmailService(configuration);
                var totalEmails = await migrateEmailService.ExecuteAsync();
                Console.WriteLine($"Migrate [Email] to [Email Service] => DONE: inserted {totalEmails} emails. \n");
            });
        }
        static void MigrateSchedule()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("Start migrate Schedule.....");
                var migrateScheduleService = new MigrationScheduleToScheduleService(configuration);
                var totalEmails = await migrateScheduleService.ExecuteAsync();
                Console.WriteLine($"Migrate {totalEmails} schedule to Schedule service");
            });
        }

        static void MigrateInterview()
        {
            Console.WriteLine("Start migrate Interviews.....");
            var migrateInterviewService = new MigrateInterviewToInterviewService();
            var totalInterview = migrateInterviewService.InsertInterviewToInterviewService(configuration).Result;
            Console.WriteLine("{0} interview(s) inserted to InterviewService", totalInterview);
        }
    }
}
