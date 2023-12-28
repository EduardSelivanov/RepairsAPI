using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repairs.API.Repositories.Token
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration con)
        {
            configuration = con;
        }


        public string CreateJWTToken(IdentityUser user, List<string> rolesOfUser)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in rolesOfUser)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience:configuration.GetSection("Jwt:Audience").Value,
                claims:claims,
                expires:DateTime.Now.AddMinutes(45),
                signingCredentials:creds
                );
            string tokenString= new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
            
        }
    }
}
