using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RepairsAPI.DBContexts
{
    public class AuthDBContext: IdentityDbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> opt):base(opt)
        {  
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var UserRoleId = "fe126829-772a-482b-9b4c-a485e5d97767";
            var RepairManRoleId = "02a38eb9-563f-4541-8ee4-c63a9a9ab6d6";
            var AdminRoleId = "b1d3338d-5570-43af-8b25-ea80e194328a";

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= UserRoleId,
                    ConcurrencyStamp=UserRoleId,
                    Name="user",
                    NormalizedName="user".ToUpper()
                },
                new IdentityRole
                {
                    Id= RepairManRoleId,
                    ConcurrencyStamp=RepairManRoleId,
                    Name="repairman",
                    NormalizedName="repairman".ToUpper()
                },
                new IdentityRole
                {
                    Id= AdminRoleId,
                    ConcurrencyStamp=AdminRoleId,
                    Name="admin",
                    NormalizedName="admin".ToUpper()
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
            
        }
    }
}
