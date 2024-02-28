using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;

namespace Domains
{
    public partial class TbLawyersMainConsultings
    {
        [Key]
        public Guid? LawyersMainConsultingsId { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل اختر اسم المحامي ")]

        public string? LawyerId { get; set; }
        public string? LawyerUserName { get; set; }
        public string? ConsultingNo { get; set; }
        public string? DaysOfWork { get; set; }
        public string? HoursOfWork { get; set; }
        public string? EvaluationNoOfStatrs { get; set; }
        public string? EvaluationNumerical { get; set; }
        public string? YearsOfExperience { get; set; }
        public string? DocumentA { get; set; }
        public string? DocumentB { get; set; }
        public string? DocumentC { get; set; }
        public string? DocumentD { get; set; }
        public string? IdentityDocument { get; set; }
        public string? ShortDescription { get; set; }
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
        public string? DeviceToken { get; set; }
        public bool IsActivated { get; set; } = true;

        [Required(ErrorMessage = "من فضلك ادخل اسم الاستشارة الرئيسية ")]
        public Guid? MainConsultingId { get; set; }
        public string? MainConsultingTitle { get; set; }
        public string? MainConsultingImage { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل قيمة استشارة ثلاثون دقيقة ")]
        public string? Consulting30MinutesCost { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل قيمة استشارة ستون دقيقة ")]
        public string? Consulting60MinutesCost { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل قيمة استشارة تسعون دقيقة ")]
        public string? Consulting90MinutesCost { get; set; }


        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }
      
    }
}
