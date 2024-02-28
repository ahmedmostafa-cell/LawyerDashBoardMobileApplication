using System.Collections.Generic;

namespace AlMohamyProject.Models
{
    public class AuthModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImgUrl { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
        public bool PhoneVerify { get; set; }
        public bool Status { get; set; }
        public bool IsUser { get; set; } = false;
        public bool IsAdmin { get; set; } = false;

        public string Message { get; set; }
        public string ArMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
