using System.ComponentModel.DataAnnotations;

namespace Repair.UI.Models.DTOsMVC.UserDTO
{
    public class UserRegDTO
    {

        public string NameUser { get; set; }


        public string PasswordUser { get; set; }

        public string[] UserRoles { get; set; }
    }
}
