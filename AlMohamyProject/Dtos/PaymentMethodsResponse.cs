using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodAr { get; set; }
        public string PaymentMethodEn { get; set; }
        public string PaymentMethodCode { get; set; }
        public bool IsDirectPayment { get; set; }
        public double ServiceCharge { get; set; }
        public double TotalAmount { get; set; }
        public string CurrencyIso { get; set; }
        public string ImageUrl { get; set; }
        public bool IsEmbeddedSupported { get; set; }
        public string PaymentCurrencyIso { get; set; }
    }

    public class PaymentMethodsResponseData
    {
        public List<PaymentMethod> PaymentMethods { get; set; }
    }

    public class PaymentMethodsResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ValidationErrors { get; set; }
        public PaymentMethodsResponseData Data { get; set; }
    }

}
