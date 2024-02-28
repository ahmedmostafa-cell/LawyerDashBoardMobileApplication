using System.ComponentModel.DataAnnotations;
using System;

namespace AlMohamyProject.Dtos
{
    public class ChargeGateDeductDtos
    {
        public Guid? PaymentGatesId { get; set; }

       
        public decimal TotalCharges { get; set; }

       
      
        public string Percent { get; set; }
    }
}
