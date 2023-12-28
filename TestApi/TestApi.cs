using Microsoft.AspNetCore.Mvc;
using Moq;
using Repairs.API.Models.DTOs.RepairDTO;
using Repairs.API.Repositories.RepairRepo;
using RepairsAPI.Controllers;

namespace TestApi
{
    public class TestApi
    {
        [Fact]
        public async Task ViewRepairsRepairMan_Okres_ValidId()
        {
            Guid repirManId = new Guid("edad1c25-ac1c-493a-8a3e-1e4ed3cfd382");

            var repairRepositoryMock = new Mock<IRepairRepository>();

            repairRepositoryMock.Setup(repo => repo.ShowROnlyForRepairManAsync(repirManId)).
                ReturnsAsync(new List<DisplayOrEditRepairForRepairManDTO>());

            RepairsController cont=new RepairsController(repairRepositoryMock.Object);

            var res = await cont.ViewAllRepairsForCertainRepairMan(repirManId);

            var okres=Assert.IsType<OkObjectResult>(res);
            Assert.NotNull(okres);

            var list = Assert.IsType<List<DisplayOrEditRepairForRepairManDTO>>(okres.Value);
            Assert.NotNull(list);
        }

        [Fact]
        public async Task EditRepairByRepairMan_OkRes_Valid_Data()
        {
            var repirManId = new Guid();
            var repairId = new Guid();
            DisplayOrEditRepairForRepairManDTO newData=new DisplayOrEditRepairForRepairManDTO();

            Mock<IRepairRepository> repairRepMock= new Mock<IRepairRepository>();
            repairRepMock.Setup(rep => rep.EditRepairByRepairManAsync(
                repirManId, repairId, newData
                )).ReturnsAsync(new DisplayOrEditRepairForRepairManDTO());

            RepairsController cont = new RepairsController(repairRepMock.Object);

            IActionResult res = await cont.EditRepairByRepairman(repirManId, repairId, newData);

            OkObjectResult okres=Assert.IsType<OkObjectResult>(res);
            Assert.NotNull(okres);

            DisplayOrEditRepairForRepairManDTO resBody=
                Assert.IsType<DisplayOrEditRepairForRepairManDTO>(okres.Value);
            Assert.NotNull(resBody);

        }
    }
}