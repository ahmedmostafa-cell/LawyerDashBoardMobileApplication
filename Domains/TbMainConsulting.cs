using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

#nullable disable

namespace Domains
{
    public partial class TbMainConsulting
    {

        public Guid? MainConsultingId { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل اسم الاستشارة الرئيسية")]
        public string MainConsultingTitle { get; set; }

        public string MainConsultingImage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل وصف الاستشارة الرئيسية")]
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
        public string Consulting30MinutesCost { get; set; }
        public string Consulting60MinutesCost { get; set; }
        public string Consulting90MinutesCost { get; set; }


    }
}
