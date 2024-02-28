using System.ComponentModel.DataAnnotations;

namespace AlMohamyProject.Dtos
{
    public class NotificationDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string userId { get; set; }
    }
}
