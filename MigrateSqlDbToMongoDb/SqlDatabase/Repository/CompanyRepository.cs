using SqlDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace SqlDatabase.Repository
{
    public class CompanyRepository
    {
        public List<Company> GetCompanies()
        {
            using (var db = new HrToolDbContext())
            {
                return db.Company.ToList();
            }
        }
    }
}
