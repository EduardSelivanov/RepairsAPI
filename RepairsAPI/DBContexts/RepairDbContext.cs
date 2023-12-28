using Microsoft.EntityFrameworkCore;

using RepairsAPI.Models.Domains;

namespace Repairs.API.DBContexts
{
    public class RepairDbContext:DbContext
    {
        public RepairDbContext(DbContextOptions<RepairDbContext> opt ):base(opt)
        { 
            
        }

        public DbSet<Repair> Repairs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repair>().HasKey(e=>e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
