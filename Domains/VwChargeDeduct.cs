using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public partial class VwChargeDeduct
    {
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? IdReciever { get; set; }

        public string? ChargeTypeSender { get; set; }

       
        public string? CreatedBy { get; set; }
        public decimal totalCharges { get; set; }
       
    }
}
