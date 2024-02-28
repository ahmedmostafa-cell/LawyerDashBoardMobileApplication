using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public partial class TbLawyerPeriodCostConsult
    {
        public Guid? LawyerPeriodCostConsultId { get; set; }
     
        public string? LawyerId { get; set; }
        public string? LawyerName { get; set; }
       
     
        public string? ConsultingPeriod { get; set; }
        public string? ConsultingPeriodCost { get; set; }
       
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
