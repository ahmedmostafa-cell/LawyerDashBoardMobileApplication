using System;

namespace AlMohamyProject.Dtos
{
    public class EaluationDTos
    {
      
        public string EvaluaterId { get; set; }
        public string ToBeEvaluatedId { get; set; }
       
        public string StartsNo { get; set; }
        public string ConsultationServiceId { get; set; }


        public string EvaluaterImage { get; set; }
        public string ToBeEvaluatedImage { get; set; }

        public string EvaluationText { get; set; }

    }
}
