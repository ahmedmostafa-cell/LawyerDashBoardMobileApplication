using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace AlMohamyProject.Dtos
{
    public class ApproveOfficeRequestDtos
    {
      
        public string UserPhoneNumber { get; set; }
      
        public string UserFirsName { get; set; }


        public IFormFile DocumentA { get; set; }

        public string ApprovedOfficeLicenseDoc { get; set; }
       
        public string UserEmail { get; set; }


        public string userid { get; set; }

    }
}
