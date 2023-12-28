using Microsoft.AspNetCore.Mvc;
using Repair.UI.Models.DTOsMVC.RepairDTO;
using Repair.UI.Models.DTOsMVC.UserDTO;
using Repair.UI.Services;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Repair.UI.Controllers
{
    public class RepairsMVCController : Controller
    {
        private readonly TokenAndUserService _tokenAndUserService;
        private readonly IHttpClientFactory _httpClientFactory;

        public RepairsMVCController(TokenAndUserService tokenAndUserService, IHttpClientFactory httpClientFactory)
        {
            _tokenAndUserService = tokenAndUserService;
            _httpClientFactory = httpClientFactory;
            //https://localhost:7035/api/Repairs/
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayForRepairMan()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            List<RepairsForRepairmanDTOMVC> listToShow = new List<RepairsForRepairmanDTOMVC>();

            UserDTOMVC repairman = _tokenAndUserService.GetUser();
            string token = _tokenAndUserService.GetToken();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string apiUrl = $"https://localhost:7035/api/Repairs/ForRepairman/{repairman.IdOfUser}";

                HttpResponseMessage responseMes=await client.GetAsync(apiUrl);
                responseMes.EnsureSuccessStatusCode();

                var listOfRepairs = await responseMes.Content.ReadFromJsonAsync<IEnumerable<RepairsForRepairmanDTOMVC>>();

                if (listOfRepairs is not null)
                {
                    listToShow.AddRange(listOfRepairs);

                }
                return View(listToShow);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Произошла ошибка: ЗАЙДИ ";
                return RedirectToAction("Index", "Home");
            }  
        }
   

        // for Client show only repairs attached to user
        [HttpGet]
        public async Task<IActionResult> DisplayMyOrders()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            List<RepairsForUserDTOMVC> listToShow = new List<RepairsForUserDTOMVC>();

            UserDTOMVC user = _tokenAndUserService.GetUser();
            string token = _tokenAndUserService.GetToken();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string urlApi = $"https://localhost:7035/api/Repairs/ForUser/{user.IdOfUser}";

                HttpResponseMessage responseMes = await client.GetAsync(urlApi);
                responseMes.EnsureSuccessStatusCode();
                var listOfRepairs = await responseMes.Content.ReadFromJsonAsync<IEnumerable<RepairsForUserDTOMVC>>();

                if(listOfRepairs is not null)
                {
                    listToShow.AddRange(listOfRepairs);
                    
                }
                return View(listToShow);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateIssueByUser(RepairsForUserDTOMVC issue)
        {
            HttpClient client= _httpClientFactory.CreateClient();
            UserDTOMVC user= _tokenAndUserService.GetUser();
            issue.IssuedById = user.IdOfUser;
            string token= _tokenAndUserService.GetToken();  
            try
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpRequestMessage message = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Repairs/CreateByUser"),
                    Content = new StringContent(JsonSerializer.Serialize(issue),Encoding.UTF8,"application/json")
                };
                HttpResponseMessage responseMes = await client.SendAsync(message);

                responseMes.EnsureSuccessStatusCode();

                var response = await responseMes.Content.ReadFromJsonAsync<Guid>();

                if (response != null)
                {
                    Console.WriteLine(response);
                    return RedirectToAction("DisplayMyOrders", "RepairsMVC");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> ShowIssueByRepairMan(Guid issueId)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            UserDTOMVC repairman= _tokenAndUserService.GetUser();
            string token= _tokenAndUserService.GetToken();  
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = $"https://localhost:7035/api/Repairs/ViewById/{issueId}";
                HttpResponseMessage responseMes = await client.GetAsync(apiUrl);
                responseMes.EnsureSuccessStatusCode();
                RepairsForRepairmanDTOMVC? repair = 
                    await responseMes.Content.ReadFromJsonAsync<RepairsForRepairmanDTOMVC>();
                if(repair!= null)
                {
                    return View(repair);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Произошла ошибка: ЗАЙДИ " ;
                return RedirectToAction("Index", "Home");
            }   
        }


        [HttpPost]//Update Issue
        public async Task<IActionResult> ShowIssueByRepairMan(RepairsForRepairmanDTOMVC singleRepair)
        {
            HttpClient client= _httpClientFactory.CreateClient();
            UserDTOMVC repairman = _tokenAndUserService.GetUser();

            string token = _tokenAndUserService.GetToken();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                singleRepair.FixedById = repairman.IdOfUser;
                
                HttpRequestMessage reqMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7035/api/Repairs/EditByRepairMan/{repairman.IdOfUser}/{singleRepair.Id}"),
                    Content=new StringContent(JsonSerializer.Serialize(singleRepair),Encoding.UTF8,"application/json")
                };

                HttpResponseMessage responseMess= await client.SendAsync(reqMess);
                responseMess.EnsureSuccessStatusCode();

                RepairsForRepairmanDTOMVC? editedrepair = 
                    await responseMess.Content.ReadFromJsonAsync<RepairsForRepairmanDTOMVC>();

                if (editedrepair != null)
                {
                    return RedirectToAction("ShowIssueByRepairMan", "RepairsMVC", new { issueId = singleRepair.Id});
                }
                else 
                {
                    return BadRequest();
                }
            }
            catch
            { 
                return BadRequest();
            }

           
        }

    
        [HttpGet]
        public async Task<IActionResult> CreateIssueByUser()
        {
            return View();
        }


        //create method for getting all repairs whre fixedByid is null and show list with edit button 

        [HttpGet]
        public async Task<IActionResult> ShowUnsolvedIssues()
        {

            HttpClient client = new HttpClient();
            List<RepairsForRepairmanDTOMVC> listUnres = new List<RepairsForRepairmanDTOMVC>();

            UserDTOMVC repairman = _tokenAndUserService.GetUser();
            string token = _tokenAndUserService.GetToken();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = "https://localhost:7035/api/Repairs/Unsolved";
                HttpResponseMessage responseMessage=await client.GetAsync(apiUrl);
                responseMessage.EnsureSuccessStatusCode();

                IEnumerable<RepairsForRepairmanDTOMVC>? listOfRepairs=
                    await responseMessage.Content.ReadFromJsonAsync<IEnumerable<RepairsForRepairmanDTOMVC>>();
                if(listOfRepairs != null)
                {
                    listUnres.AddRange(listOfRepairs);
                }
                return View(listUnres);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
