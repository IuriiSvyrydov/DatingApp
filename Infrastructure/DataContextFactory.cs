using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    public class DataContextFactory: IDesignTimeDbContextFactory<DataDbContext>
    {
        public DataDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-7CB8L9L\\SQLEXPRESS;Database=DataDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new DataDbContext(optionsBuilder.Options);
        }
    }
}
