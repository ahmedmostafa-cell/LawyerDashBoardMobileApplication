using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlMohamyProject.Models
{
    public class ForgotPasswordViewModel
    {
        
        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }

        public string phonenumber { get; set; }



        public string NewPassword { get; set; }


        public string RepeatNewPassword { get; set; }


        public string token { get; set; }

    }
}
