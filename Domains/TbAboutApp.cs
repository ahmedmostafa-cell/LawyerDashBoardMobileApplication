using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbAboutApp
    {
        public Guid? AboutAppId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عنوان عن التطبيق")]
        public string AboutAppTitle { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل وصف عن التطبيق")]
        public string AboutAppDescription { get; set; }
        [Required(ErrorMessage = "من فضلك اختر موجهة لمن")]
        public string AboutAppForWhom { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
