using System.Collections.Generic;
using System;

namespace AlMohamyProject.Dtos
{
    public class LawyerAppointDtos
    {

      

        public string LawyerId { get; set; }

      

      
       
      
        public List<DayDetails> DayDetails { get; set; }


       
    }
    public class DayDetails
    {
        public string WeekDay { get; set; }
        public string FromHour { get; set; }
        public string MorEveFrst { get; set; }
        public string ToHour { get; set; }

        public string MorEveScond { get; set; }


        public string Date { get; set; }
    }
}
