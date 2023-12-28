using System.ComponentModel.DataAnnotations;

namespace Repairs.API.Models.DTOs.UserDTO
{
    public class UserRegReqDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string NameUser { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }

        public string[] UserRoles { get; set; }
    }
}
