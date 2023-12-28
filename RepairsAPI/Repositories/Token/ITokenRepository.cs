using Microsoft.AspNetCore.Identity;

namespace Repairs.API.Repositories.Token
{
    public interface ITokenRepository
    {

        public string CreateJWTToken(IdentityUser user, List<string> rolesOfUser);
    }
}
