using System;
using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class LawyerConsultDtos
    {
        public string LawyerId { get; set; }






        public List<LawyerConsult> LawyerConsultDetails { get; set; }
    }
    public class LawyerConsult
    {
        public Guid MainConsultingId { get; set; }
        public string Consulting30MinutesCost { get; set; }
        public string Consulting60MinutesCost { get; set; }
        public string Consulting90MinutesCost { get; set; }


       
    }
}
