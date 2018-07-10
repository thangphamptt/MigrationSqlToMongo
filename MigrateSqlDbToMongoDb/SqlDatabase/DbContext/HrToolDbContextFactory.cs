using Microsoft.EntityFrameworkCore;

namespace SqlDatabase.Model
{
    public class HrToolDbContextFactory
    {
        public static HrToolDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HrToolDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Ensure that the SQL database and sechema is created!
            var context = new HrToolDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }       
    }
}
