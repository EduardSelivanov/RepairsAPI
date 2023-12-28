using Repair.UI.Models.DTOsMVC.UserDTO;

namespace Repair.UI.Services
{
    public class TokenAndUserService
    {
        private string _authToken;
        private UserDTOMVC _authUser;

        public void CreateToken(string token)
        {
            _authToken = token;
        }

        public string GetToken(){
            return _authToken;
        }

        public void CreateUser(UserDTOMVC authUser)
        {
            _authUser = authUser;
        }
        public UserDTOMVC GetUser()
        {
            return _authUser;
        }

    }
}
