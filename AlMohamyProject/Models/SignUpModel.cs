using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AlMohamyProject.Models
{
    public class SignUpModel
    {
        public string Password { get; set; }
        public IFormFile PersonalImage { get; set; }

        public string ShortDescription { get; set; }

        public string PhoneNumber { get; set; }
        public string ImageProfile { get; set; }

        public IFormFile DocumentA { get; set; }


        public IFormFile IdentityDocumentA { get; set; }

        public string IdentityDocument { get; set; }

        public string DocumentAName { get; set; }

        public IFormFile DocumentB { get; set; }



        public string DocumentBName { get; set; }


        public IFormFile DocumentC { get; set; }



        public string DocumentCName { get; set; }



        public IFormFile DocumentD { get; set; }



        public string DocumentDName { get; set; }
        public string Email { get; set; }

        public string UserType { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }

        public string FamilyName { get; set; }


        public string Id { get; set; }


        public string OTP { get; set; }



        public string? IdentityId { get; set; }



    }
}
