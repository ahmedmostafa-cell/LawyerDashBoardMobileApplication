using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbConsultingType
    {
        public Guid? ConsultingTypeId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عنوان نوع الاستشارة")]
        public string ConsultingTypeTitle { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل تفاصيل نوع الاستشارة")]
        public string ConsultingTypeDescription { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
