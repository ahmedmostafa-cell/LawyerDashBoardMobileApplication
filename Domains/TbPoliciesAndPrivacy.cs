using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbPoliciesAndPrivacy
    {
        public Guid? PoliciesAndPrivacyId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عنوان  السياسة و الخصوصية")]
        public string PoliciesAndPrivacyTitle { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل تفاصيل  السياسة و الخصوصية")]
        public string PoliciesAndPrivacyDescription { get; set; }
        [Required(ErrorMessage = "من فضلك اختر موجهة لمن")]
        public string PoliciesAndPrivacyForWhom { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
