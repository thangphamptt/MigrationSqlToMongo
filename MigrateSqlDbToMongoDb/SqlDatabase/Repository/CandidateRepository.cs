using SqlDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace SqlDatabase.Repository
{
    public class CandidateRepository
    {
        public CandidateRepository()
        {

        }
        public List<Candidate> GetCandidates()
        {
            using (var db = new HrToolDbContext())
            {
                return db.Candidate.ToList();               
            }
        }
    }
}
