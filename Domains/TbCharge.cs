using System;
using System.Collections.Generic;

#nullable disable

namespace Domains
{
    public partial class TbCharge
    {
        public Guid? ChargeId { get; set; }
        public string IdSender { get; set; }

        public string SenderName { get; set; }

        public string IdReciever { get; set; }

        public string RecieverName { get; set; }

        public Guid? ConsultingId { get; set; }

        public string ChargeTypeSender { get; set; }

        public string ChargeTypeReciever { get; set; }
        public string ChargeValue { get; set; }


        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }


        public string MyfatoorahInvoiceId { get; set; }
    }
}
