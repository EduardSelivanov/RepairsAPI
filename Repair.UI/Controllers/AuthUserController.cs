using Microsoft.AspNetCore.Mvc;
using Repair.UI.Models.DTOsMVC.UserDTO;
using Repair.UI.Services;
using System.Text;
using System.Text.Json;

namespace Repair.UI.Controllers
{
    public class AuthUserController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly TokenAndUserService _tokenService;

        public AuthUserController(IHttpClientFactory clFac, TokenAndUserService tokenser)
        {
            clientFactory = clFac;
            _tokenService= tokenser;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegDTO asuumedUser)
        {
            HttpClient client = clientFactory.CreateClient();

            HttpRequestMessage reqMess = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri=new Uri("https://localhost:7035/api/Auth/Register"),
                Content=new StringContent(JsonSerializer.Serialize(asuumedUser),Encoding.UTF8,"application/json")
            };
            var responseMes=await client.SendAsync(reqMess);
            responseMes.EnsureSuccessStatusCode();

            var response=await responseMes.Content.ReadFromJsonAsync<UserRegDTO>();

            if(response is not null)
            {
                return RedirectToAction("Index","home");
            }
            else
            {
                return View();
            }

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpGet]
        public  IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginReqDTOMVC asumedUser)
        {
            HttpClient client = clientFactory.CreateClient();

            HttpRequestMessage reqMess = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7035/api/Auth/Login"),
                Content = new StringContent(JsonSerializer.Serialize(asumedUser), Encoding.UTF8, "application/json")
            };

            var responseMes = await client.SendAsync(reqMess);
            responseMes.EnsureSuccessStatusCode();

            LoginTokenDTOMVC? tokenAndUser = await responseMes.Content.ReadFromJsonAsync<LoginTokenDTOMVC>();

            if (tokenAndUser is not null)
            {
                string tokenString = tokenAndUser.JWTTOken;
               
                _tokenService.CreateToken(tokenString);
                _tokenService.CreateUser(tokenAndUser.MyUser);
                
                return RedirectToAction("Index", "home");
            }
            else
            {
                return View();
            }
        }
        
    }
}
