using System.ComponentModel.DataAnnotations;
using System;

namespace AlMohamyProject.Models
{
    public class MainConsultingModel
    {
        public Guid MainConsultingId { get; set; }

        [Required(ErrorMessage = "Please Enter MainConsultingTitle")]
        public string MainConsultingTitle { get; set; }



        [Required(ErrorMessage = "Please Enter MainConsultingImage")]
        public string MainConsultingImage { get; set; }




        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required(ErrorMessage = "Please Enter MainConsultingDescription")]
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
        public string Consulting30MinutesCost { get; set; }
        public string Consulting60MinutesCost { get; set; }
        public string Consulting90MinutesCost { get; set; }
    }
}
