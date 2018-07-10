using SqlDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace SqlDatabase.Repository
{
    public class JobRepository
    {
        public List<Job> GetJobs()
        {
            using (var db = new HrToolDbContext())
            {
                return db.Job.ToList();
            }
        }
    }
}
