using AutoMapper;
using Repairs.API.Models.DTOs.RepairDTO;
using RepairsAPI.Models.Domains;

namespace Repairs.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Repair, CreateRepairDTO>().ReverseMap();
            CreateMap<Repair, DisplayRepairForUserDTO>().ReverseMap();
            CreateMap<Repair, DisplayOrEditRepairForRepairManDTO>().ReverseMap();
        }
    }
}
