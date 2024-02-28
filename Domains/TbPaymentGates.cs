using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public partial class TbPaymentGates
    {
        public Guid? PaymentGatesId { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل اسم بوابة الدفع ")]
        public string? PaymentGateTitle { get; set; }
      
        public string? PaymentGateImage { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل حالة تفعيل بوابة الدفع ")]
        public string? ActivationStatus { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عمولة بوابة الدفع ")]
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
       
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }

       

    }
}
