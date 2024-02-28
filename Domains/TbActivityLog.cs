using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public partial  class TbActivityLog
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }

        public string? UserId { get; set; }
        public string? Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
