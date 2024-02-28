using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public partial class TbServices
    {
        public Guid? ServiceId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اسم الخدمة")]
        public string? ServiceName { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }

        public int? CurrentState { get; set; }
    }
}
