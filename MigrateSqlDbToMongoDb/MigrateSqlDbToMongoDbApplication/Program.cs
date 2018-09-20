using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Services;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
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
            Console.Title = "Migration HrToolv1 To ResponsiveHR Application";
        }

        static void Main(string[] args)
        {
            Configuration();

            var hrtoolDbContext = new HrToolv1DbContext(configuration);
            var candidateDbContext = new CandidateDbContext(configuration);
            var interviewDbContext = new InterviewDbContext(configuration);
            var jobDbContext = new JobDbContext(configuration);
            var jobMatchingDbContext = new JobMatchingDbContext(configuration);
            var offerDbContext = new OfferDbContext(configuration);
            var templateDbContext = new TemplateDbContext(configuration);
            var scheduleDbContext = new ScheduleDbContext(configuration);

            var migrateOrganizationalUnitService = new MigrateOrganizationalUnitService(
                configuration,
                hrtoolDbContext,
                candidateDbContext,
                interviewDbContext,
                jobDbContext,
                jobMatchingDbContext,
                offerDbContext,
                templateDbContext);

            var migrateCandidateService = new MigrateCandidateService(configuration,
                hrtoolDbContext,
                candidateDbContext,
                interviewDbContext,
                jobMatchingDbContext,
                offerDbContext,
                scheduleDbContext);

            var migrateApplicationService = new MigrateApplicationService(configuration,
                hrtoolDbContext,
                candidateDbContext,
                interviewDbContext);

            var migrateInterviewService = new MigrateInterviewService(configuration, hrtoolDbContext, interviewDbContext);

            var migrateScheduleService = new MigrateScheduleService(configuration, hrtoolDbContext,
                scheduleDbContext, interviewDbContext);

            var migrateJobService = new MigrateJobService(configuration, hrtoolDbContext,
                jobDbContext, candidateDbContext,
                interviewDbContext, offerDbContext, 
                jobMatchingDbContext);

            var migrateOfferService = new MigrateOfferService(configuration, hrtoolDbContext, offerDbContext);

            var migrateTemplateService = new MigrateTemplateService(configuration, hrtoolDbContext,
                templateDbContext, candidateDbContext,
                interviewDbContext);

            var emailDbContext = new EmailDbContext(configuration);
            var migrateEmailService = new MigrateEmailService(configuration, hrtoolDbContext, 
                emailDbContext, candidateDbContext);

            var migrateNestedApplicationIntoCandidateService = new MigrateNestedApplicationIntoCandidateService(candidateDbContext);

            Task.Run(async () =>
            {
                //await migrateOrganizationalUnitService.ExecuteAsync();
                //await migrateCandidateService.ExecuteAsync();
                //await migrateApplicationService.ExecuteAsync();
                //await migrateInterviewService.ExecuteAsync();
                //await migrateScheduleService.ExecuteAsync();
                //await migrateJobService.ExecuteAsync();
                //await migrateOfferService.ExecuteAsync();
                //await migrateTemplateService.ExecuteAsync();
                //await migrateEmailService.ExecuteAsync();
                //await new MigrateAttachmentService(configuration, candidateDbContext).ExecuteAsync();

                await migrateNestedApplicationIntoCandidateService.ExecuteAsync();

                Console.WriteLine("\n MIGRATION COMPLETED !!");
            });

            Console.ReadKey();
        }
    }
}
