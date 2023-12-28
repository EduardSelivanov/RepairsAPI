namespace Repairs.API.Models.DTOs.UserDTO
{
    public class UsersTokenAndUserDTO
    {
        public string JWTTOken { get; set; }

        public UserDTO MyUser { get; set; }
    }
}
