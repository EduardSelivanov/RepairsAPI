using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repairs.API.Models.DTOs.RepairDTO;
using Repairs.API.Repositories.RepairRepo;

namespace RepairsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairsController : ControllerBase
    {
        private readonly IRepairRepository _repairRepository;

        public RepairsController(IRepairRepository repRep)
        {
            _repairRepository= repRep;
        }

        [HttpPost]
        [Route("CreateByUser")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateRepair([FromBody] CreateRepairDTO repairByUser)
        {
           if(repairByUser != null) 
            {
                Guid idicko=await _repairRepository.CreateRepairAsync(repairByUser);
               
                return Ok(idicko);
            }
            return null;
        }



        // access only by repairmanRole Guid RepairManId
        [HttpGet]
        [Route("ForRepairman/{repairManId}")]
        [Authorize(Roles ="repairman")]
        public async Task<IActionResult> ViewAllRepairsForCertainRepairMan(Guid repairManId)
        {
            if (repairManId != Guid.Empty)
            {
                List<DisplayOrEditRepairForRepairManDTO> showListForRepairmanDTO=
                    await _repairRepository.ShowROnlyForRepairManAsync(repairManId);
                //return BadRequest();
                return Ok(showListForRepairmanDTO);
            }
            else
            {
                return BadRequest("Invalid userId");
            }
        }


        // access for authorized user 
        [HttpGet]
        [Route("ForUser/{userId}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ViewAllRepairsForCertainClient([FromRoute] Guid userId)
        {
            if (userId != Guid.Empty)
            {
                List<DisplayRepairForUserDTO> showwList = await _repairRepository.ShowOnlyForCertainUserAsync(userId);
                return Ok(showwList);
            }
            else 
            {
                return BadRequest("Invalid userId");
            }
        }



        [HttpPut]
        [Route("EditByRepairMan/{repairManId}/{repairId}")]
        [Authorize(Roles ="repairman")]
        public async Task<IActionResult> EditRepairByRepairman([FromRoute]Guid repairManId,[FromRoute] Guid repairId,
            [FromBody] DisplayOrEditRepairForRepairManDTO editByRepairMan)
        {
            if(repairId!=null)
            {
                DisplayOrEditRepairForRepairManDTO objToDisplay = 
                    await _repairRepository.EditRepairByRepairManAsync(repairManId,repairId,editByRepairMan);
                return Ok(objToDisplay);

            }
            else
            {
                return null;
            }
        }



        [HttpGet]
        [Route("ViewById/{repairId}")]
        [Authorize(Roles = "repairman")]
        public async Task<IActionResult> ViewCeratinRepair([FromRoute] Guid repairId)
        {
            if (repairId != Guid.Empty)
            {
                DisplayOrEditRepairForRepairManDTO repairDTO= await _repairRepository.ShowRepairByIdAsync(repairId);
                return Ok(repairDTO);
            }
            else
            {
                return BadRequest("Id is null");
            }
        }

        [HttpGet]
        [Route("Unsolved")]
        [Authorize(Roles = "repairman")]
        public async Task<IActionResult> ViewUnsolvedRepairs()
        {
            List<DisplayOrEditRepairForRepairManDTO> unresRepDTO = await _repairRepository.ShowUnresRepairsAsync();

            return Ok(unresRepDTO);
            



        }
    }
}
