using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class Product
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public bool IsDigital { get; set; }
        public object ImageSrc { get; set; }
        public object SpecificVat { get; set; }
        public object ProductCost { get; set; }
    }

    public class GatewayOrderRequest
    {
        public decimal Amount { get; set; }
        public string OrderNumber { get; set; }
        public string CallBackUrl { get; set; }
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }
        public string ClientMobile { get; set; }
        public string Note { get; set; }
        public object CancelUrl { get; set; }
        public List<Product> Products { get; set; }
        public object Currency { get; set; }
    }

    public class PaymentError
    {
        public string ErrorCode { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }
        public long ErrorTime { get; set; }
    }
    public class InvoiceDetailsDtos
    {
       

       
            public GatewayOrderRequest GatewayOrderRequest { get; set; }
            public decimal Amount { get; set; }
            public string TransactionNo { get; set; }
            public string OrderStatus { get; set; }
            public List<PaymentError> PaymentErrors { get; set; }
            public string Url { get; set; }
            public string QrUrl { get; set; }
            public string CheckUrl { get; set; }
            public bool Success { get; set; }
            public bool DigitalOrder { get; set; }
            public object ForeignCurrencyRate { get; set; }
       
    }
}
