using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repairs.API.Models.DTOs.UserDTO;
using Repairs.API.Repositories.Token;

namespace RepairsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenMaker;

        public AuthController(UserManager<IdentityUser> userMan, ITokenRepository tokenMake)
        {
           _userManager = userMan;
            _tokenMaker = tokenMake;
        }

        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegReqDTO assumedUserDTO)
        {
            IdentityUser newUser = new IdentityUser()
            {
                Email=assumedUserDTO.NameUser,
                UserName=assumedUserDTO.NameUser
            };
            var identityRes=await _userManager.CreateAsync(newUser,assumedUserDTO.PasswordUser);
            if(identityRes.Succeeded)
            {
                if (assumedUserDTO.UserRoles != null && assumedUserDTO.UserRoles.Any())
                {
                    identityRes=await _userManager.AddToRolesAsync(newUser, assumedUserDTO.UserRoles);
                    
                    if (identityRes.Succeeded)
                    {
                        return Ok(assumedUserDTO);
                    }
                }
            }
            return BadRequest("Somethnig Wrong");
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogReqDTO assumedUserDTO)
        {
            IdentityUser guessedUser = await _userManager.FindByEmailAsync(assumedUserDTO.UserName);
            if(guessedUser is not null)
            {
                bool checkPass=await _userManager.CheckPasswordAsync(guessedUser,assumedUserDTO.Password);
                if (checkPass)
                {
                    IList<string> guessedUserRoles = await _userManager.GetRolesAsync(guessedUser);

                    if(guessedUserRoles  is not null)
                    {
                        //Create token
                        string token = _tokenMaker.CreateJWTToken(guessedUser, guessedUserRoles.ToList());

                        UsersTokenAndUserDTO response = new UsersTokenAndUserDTO
                        {
                            JWTTOken = token,
                            MyUser=new UserDTO
                            {
                                IdOfUser=Guid.Parse(guessedUser.Id),
                                NameOfUser=guessedUser.UserName,
                                EmailOfUser=guessedUser.Email
                            }
                            
                        };
                        return Ok(response);
                    }
                }
            }
            return null;
        }

    }
}
