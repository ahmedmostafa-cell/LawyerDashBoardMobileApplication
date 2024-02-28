using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BL
{
    public class ApplicationUser:IdentityUser
    {
        public string? ConsultingNo { get; set; }
        public string? DaysOfWork { get; set; }

        public string? HoursOfWork { get; set; }

        public string? MainConsultingId { get; set; }

        public string? MainConsultingName { get; set; }

        public string? EvaluationNoOfStatrs { get; set; }

        public string? EvaluationNumerical { get; set; }

        public string? YearsOfExperience { get; set; }


        public string? DocumentA { get; set; }

        public string? DocumentB { get; set; }


        public string? DocumentC { get; set; }


        public string? DocumentD { get; set; }


        public string? IdentityDocument { get; set; }


        public string? IdentityId { get; set; }

        public string? ShortDescription { get; set; }

        public string? Cost { get; set; }


        public string? OTP { get; set; }

        public string? Image { get; set; }



        public string? FirstName { get; set; }

        public string? FamilyName { get; set; }

        public string? UserType { get; set; }

        public string? Status { get; set; }
      
        [NotMapped]
        public string? RoleId { get; set; }
        [NotMapped]
        public string? Role { get; set; }

        [NotMapped]
        public List<SelectListItem>? RoleList { get; set; }


        [NotMapped]
        public List<SelectListItem>? RoleList2 { get; set; }
        [NotMapped]
        public List<string>? RoleList3 { get; set; }


        [NotMapped]
        public IEnumerable<IdentityRole>? RoleListMain { get; set; }


        public string DeviceToken { get; set; }

        public bool IsActivated { get; set; } = true;


        public bool IsApprovedOffice { get; set; } = true;


    }
}
