using System.ComponentModel.DataAnnotations;

namespace AlMohamyProject.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


        public string DeviceToken { get; set; }
    }
}
