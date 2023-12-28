using Microsoft.AspNetCore.Identity;

namespace Repairs.API.Models.DTOs.RepairDTO
{
    public class CreateRepairDTO
    {
        public string RepairName { get; set; }

        public string DescriptionRepair { get; set; }
        public Guid? IssuedById { get; set; }
        public Guid? FixedById { get; set; }

        //public IdentityUser? IssuedBy { get; set; }
    }
}
