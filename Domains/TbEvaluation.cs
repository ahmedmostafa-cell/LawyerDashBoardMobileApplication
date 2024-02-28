using System;
using System.Collections.Generic;

#nullable disable

namespace Domains
{
    public partial class TbEvaluation
    {
        public Guid? EvaluationId { get; set; }
        public string EvaluaterId { get; set; }
        public string ToBeEvaluatedId { get; set; }
        public string EvaluationText { get; set; }
        public string StartsNo { get; set; }
        public string ConsultationServiceId { get; set; }
        public string EvaluaterImage { get; set; }
        public string ToBeEvaluatedImage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
