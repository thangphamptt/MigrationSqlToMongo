using SqlDatabase.Model;
using System;
using System.Linq;

namespace MigrateSqlDbToMongoDbApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new HrToolDbContext())
            {
                var data = db.Candidate.ToList();
                Console.WriteLine("Candidate count: {0}", db.Candidate.Count());
                Console.WriteLine();
            }
        }
    }
}
