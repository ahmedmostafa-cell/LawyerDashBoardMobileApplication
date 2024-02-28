using System;
using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class TransactionElement
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

        public string ConsultingDateAndTime { get; set; }
        public string RequestNo { get; set; }

    }
    public class ChargeDtos
    {
        public string NetChargeValue { get; set; }

        public string AppProfit { get; set; }
        public List<TransactionElement> lstTransactionElements { get; set; }
    }
}
