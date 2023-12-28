using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repairs.API.DBContexts;
using Repairs.API.Models.DTOs.RepairDTO;
using RepairsAPI.Models.Domains;

namespace Repairs.API.Repositories.RepairRepo
{



    public class RepairRepository : IRepairRepository
    {
        private readonly IMapper _mapper;
        private readonly RepairDbContext _contextRepDB;

        public RepairRepository(IMapper mapper,RepairDbContext context)
        {
            _mapper = mapper;
            _contextRepDB = context;
        }

        public async Task<Guid> CreateRepairAsync(CreateRepairDTO repairDTO)
        {
            // target= <Target>(Source)
            Repair repair= _mapper.Map<Repair>(repairDTO);

            await _contextRepDB.Repairs.AddAsync(repair);
            await _contextRepDB.SaveChangesAsync();


            return repair.Id;

        }

        public async Task<DisplayOrEditRepairForRepairManDTO> EditRepairByRepairManAsync(Guid repairManId, Guid repairId,
            DisplayOrEditRepairForRepairManDTO editByRepairMan)
        {
            Repair? existingRepair = await _contextRepDB.Repairs.FirstOrDefaultAsync(r => r.Id == repairId);
            if (existingRepair != null)
            {
                //existingRepair = _mapper.Map<Repair>(editByRepairMan);
                existingRepair.Id = editByRepairMan.id;
                existingRepair.RepairName = editByRepairMan.RepairName;
                existingRepair.DescriptionRepair=editByRepairMan.DescriptionRepair;
                existingRepair.IssuedById = editByRepairMan.IssuedById;
                existingRepair.FixedById = repairManId;

                await _contextRepDB.SaveChangesAsync();
                editByRepairMan = _mapper.Map<DisplayOrEditRepairForRepairManDTO>(existingRepair);

                return editByRepairMan;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<DisplayRepairForUserDTO>> ShowOnlyForCertainUserAsync(Guid userId)
        {
            List<Repair> listForUser = await _contextRepDB.Repairs.Where(r=>r.IssuedById==userId).ToListAsync();

            List<DisplayRepairForUserDTO> listForUserDTO =_mapper.Map<List<DisplayRepairForUserDTO>>(listForUser);

            return listForUserDTO;
        }

        public async Task<DisplayOrEditRepairForRepairManDTO> ShowRepairByIdAsync(Guid repairId)
        {
            Repair concreteRepair=await _contextRepDB.Repairs.Where(r=>r.Id==repairId).FirstOrDefaultAsync();
            DisplayOrEditRepairForRepairManDTO concreterRepairDTO = 
                _mapper.Map<DisplayOrEditRepairForRepairManDTO>(concreteRepair);

            return concreterRepairDTO;
        }

        public async Task<List<DisplayOrEditRepairForRepairManDTO>> ShowROnlyForRepairManAsync(Guid repairManId)
        {
            List<Repair> listForRepairMan = await _contextRepDB.Repairs.Where(u => u.FixedById == repairManId).ToListAsync();
            List<DisplayOrEditRepairForRepairManDTO> listForRepairManDTO =
                _mapper.Map<List<DisplayOrEditRepairForRepairManDTO>>(listForRepairMan);
            return listForRepairManDTO;

        }

        public async Task<List<DisplayOrEditRepairForRepairManDTO>> ShowUnresRepairsAsync()
        {
            List<Repair> unresRepairs = await _contextRepDB.Repairs.Where(r=>r.FixedById==null).ToListAsync();
            List<DisplayOrEditRepairForRepairManDTO> unresRepairsDTO=
                _mapper.Map<List<DisplayOrEditRepairForRepairManDTO>>(unresRepairs);
            return unresRepairsDTO;
        }
    }
}
