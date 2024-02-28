using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
   
    public class LawyerScheduleDtos
    {
        public string LawyerId { get; set; }
        public List<DayDetails> DayDetails { get; set; }
    }
  
}
