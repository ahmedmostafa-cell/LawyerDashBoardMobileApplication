using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public partial class TbEvaluationApprovedOffice
    {
        public Guid? EvaluationApprovedOfficeId { get; set; }
        public string? EvaluaterId { get; set; }

        public string? EvaluaterName { get; set; }

        public string? EvaluationApprovedOfficeText { get; set; }
        public string? StartsNo { get; set; }
        public string? ApprovedOfficeId { get; set; }

        public string? ApprovedOfficeName { get; set; }
        public string? EvaluaterImage { get; set; }
        public string? ApprovedOfficeLogo { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
