using SqlDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace SqlDatabase.Repository
{
    public class ApplicationRepository
    {
        public List<JobApplication> GetJobApplications()
        {
            using (var db = new HrToolDbContext())
            {
                return db.JobApplication.ToList();
            }
        }
    }
}
