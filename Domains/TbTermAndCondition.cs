using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbTermAndCondition
    {
        public Guid? TermsAndConditionsId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عنوان  الشروط و الاحكام")]
        public string TermsAndConditionsTitle { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل تفاصيل  الشروط و الاحكام")]
        public string TermsAndConditionsDescription { get; set; }
        [Required(ErrorMessage = "من فضلك اختر موجهة لمن")]
        public string TermsAndConditionsForWhom { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
