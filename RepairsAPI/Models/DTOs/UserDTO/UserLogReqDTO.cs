using System.ComponentModel.DataAnnotations;

namespace Repairs.API.Models.DTOs.UserDTO
{
    public class UserLogReqDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
