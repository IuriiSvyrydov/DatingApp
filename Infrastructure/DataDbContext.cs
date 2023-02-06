using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure
{
    public class DataDbContext: DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext>options):base(options)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }
    }
}
