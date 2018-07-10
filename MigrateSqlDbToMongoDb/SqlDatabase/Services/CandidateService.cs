using SqlDatabase.Model;
using SqlDatabase.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDatabase.Services
{
    public class CandidateService
    {
        public CandidateService() { }

        public List<Candidate> GetCandidates()
        {
            var repository = new CandidateRepository();
            return repository.GetCandidates();
        }
    }
}
