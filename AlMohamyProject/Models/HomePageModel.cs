using BL;
using Domains;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlMohamyProject.Models
{
    public class HomePageModel
    {
        #region Declaration
        public IEnumerable<ApplicationUser> UserData { get; set; }
        public ApplicationUser OneUser { get; set; }
      

        public string ImageProfile { get; set; }

        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

      

       


        public IEnumerable<ApplicationUser> lstUsers { get; set; }

        public IEnumerable<TbMainConsulting> lstMainConsultings { get; set; }

        public IEnumerable<TbAboutApp> lstAboutApp { get; set; }

        public TbAboutApp oTbAboutApp { get; set; }

        public IEnumerable<TbApprovedOffice> lstApprovedOffices { get; set; }

        public IEnumerable<TbCharge> lstCharges { get; set; }


        public IEnumerable<TbComplainsAndSuggestion> lstComplainsAndSuggestions { get; set; }


        public IEnumerable<TbConsultingEstablish> lstConsultingEstablishs { get; set; }


        public IEnumerable<TbConsultingType> lstConsultingTypes { get; set; }


        public IEnumerable<TbDocumentationOfContract> lstDocumentationOfContracts { get; set; }

        public IEnumerable<TbEvaluation> lstEvaluations { get; set; }

        public IEnumerable<TbNotification> lstNotifications { get; set; }

        public IEnumerable<TbOffer> lstOffers { get; set; }

        public IEnumerable<TbPoliciesAndPrivacy> lstPoliciesAndPrivacy { get; set; }

        public IEnumerable<TbPromocode> lstPromocodes { get; set; }

        public IEnumerable<TbSetting> lstSettings { get; set; }


        public IEnumerable<TbSubMainConsulting> lstSubMainConsultings { get; set; }

        public IEnumerable<TbTermAndCondition> lstTermAndConditions { get; set; }


        public IEnumerable<IdentityUserRole<string>> lstUserRole { get; set; }


        public IEnumerable<TbLawyerPeriodCostConsult> lstLawyerPeriodCostConsults { get; set; }



        public IEnumerable<TbLogHistory> lstLogHistories { get; set; }

        public IEnumerable<TbPaymentGates> lstPaymentGates { get; set; }


        public IEnumerable<TbChat> lstChats { get; set; }

        public IEnumerable<TbLawyerAppintments> lstLawyerAppintments { get; set; }

        public IEnumerable<TbEvaluationApprovedOffice> lstEvaluationApprovedOffices { get; set; }

        public IEnumerable<TbCity> lstCities { get; set; }

        public IEnumerable<TbArea> lstAreas { get; set; }


        public IEnumerable<TbServices> lstServices { get; set; }


        public IEnumerable<TbActivityLog> lstActivityLogs { get; set; }


        public IEnumerable<TbLawyersMainConsultings> lstLawyersMainConsultings { get; set; }






        #endregion
    }
}
