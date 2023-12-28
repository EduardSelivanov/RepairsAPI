using Microsoft.AspNetCore.Mvc;
using Repairs.API.Models.DTOs.RepairDTO;
using RepairsAPI.Models.Domains;

namespace Repairs.API.Repositories.RepairRepo
{
    public interface IRepairRepository
    {
        Task<Guid> CreateRepairAsync(CreateRepairDTO repairDTO);
        Task<List<DisplayRepairForUserDTO>> ShowOnlyForCertainUserAsync(Guid userId);
        Task<List<DisplayOrEditRepairForRepairManDTO>> ShowROnlyForRepairManAsync(Guid repairManId);
        Task<DisplayOrEditRepairForRepairManDTO> EditRepairByRepairManAsync(Guid repairManId,Guid repairId,
            DisplayOrEditRepairForRepairManDTO editByRepairMan);

        Task<DisplayOrEditRepairForRepairManDTO> ShowRepairByIdAsync(Guid repairId);

        Task<List<DisplayOrEditRepairForRepairManDTO>> ShowUnresRepairsAsync();
    }
}
