using Microsoft.AspNetCore.Identity;

namespace RepairsAPI.Models.Domains
{
    public class Repair
    {
        public Guid Id { get; set; }
        public string RepairName { get; set; }

        public string DescriptionRepair { get; set; }
        public Guid? IssuedById { get; set; }    
        public Guid? FixedById { get; set; } 

        //public IdentityUser? IssuedBy { get; set; }

        //public IdentityUser? RepairMan { get; set; }



        
    }
}
