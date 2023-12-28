namespace Repair.UI.Models.DTOsMVC.RepairDTO
{
    public class RepairsForUserDTOMVC
    {
        
        public string RepairName { get; set; }

        public string DescriptionRepair { get; set; }
        public Guid? IssuedById { get; set; }

        public Guid? FixedById { get; set; }

       
    }
}
