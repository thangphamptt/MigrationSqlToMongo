using Microsoft.Extensions.Configuration;
using SqlDatabase.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication
{
    class Program
    {
        private static IConfiguration Configuration;
        private static void Configure()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();


        }


        static void Main(string[] args)
        {
            Configure();
            var companyId = Configuration.GetSection("CompanySetting:Id").Value;
            var companyName = Configuration.GetSection("CompanySetting:Name").Value;


            using (var db = new HrToolDbContext())
            {
                var data = db.Candidate.ToList();
                Console.WriteLine("Candidate count: {0}", db.Candidate.Count());
                Console.WriteLine();
            }
            var organizationalUnitService = new MigrateSqlDbToMongoDbApplication.OrganizationalUnitService.OrganizationalUnitService(Configuration);
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
                Console.WriteLine("Can not add OrganizationalUnit");
            }
            Console.ReadKey();
        }
    }
}
