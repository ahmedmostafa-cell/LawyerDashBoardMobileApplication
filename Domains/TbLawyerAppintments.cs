using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public partial class TbLawyerAppintments
    {
        public Guid? LawyerAppointmentId { get; set; }
       
        public string? LawyerId { get; set; }
     
        public string? LawyerName { get; set; }
       
        public string? WeekDay { get; set; }
        public double? FromHour { get; set; }
        public string? MorEveFrst { get; set; }
        public double? ToHour { get; set; }

        public string? MorEveScond { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
