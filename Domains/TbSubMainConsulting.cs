using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbSubMainConsulting
    {
        public Guid? SubMainConsultingId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عنوان الاستشارة الفرعية")]
        public string SubMainConsultingTitle { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل تفاصيل الاستشارة الفرعية")]
        public string SubMainConsultingDescription { get; set; }
        public string SubMainConsultingImage { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل قسم الاستشارة الرئيسية")]
        public Guid? MainConsultingId { get; set; }
        public string SubMainConsulting { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
        public string MainConsultingTitle { get; set; }
    }
}
