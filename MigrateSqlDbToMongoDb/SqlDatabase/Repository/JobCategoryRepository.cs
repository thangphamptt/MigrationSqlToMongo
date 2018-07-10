using SqlDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace SqlDatabase.Repository
{
    public class JobCategoryRepository
    {
        public List<Position> GetJobCategories()
        {
            using (var db = new HrToolDbContext())
            {
                return db.Position.ToList();
            }
        }
    }
}
