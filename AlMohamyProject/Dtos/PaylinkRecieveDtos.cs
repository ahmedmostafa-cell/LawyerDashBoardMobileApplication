using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class Product1
    {
        public string title { get; set; }
        public double price { get; set; }
        public int qty { get; set; }
        public string description { get; set; }
        public bool isDigital { get; set; }
        public string imageSrc { get; set; }
        public double specificVat { get; set; }
        public double productCost { get; set; }
    }

    public class GatewayOrderRequest1
    {
        public double amount { get; set; }
        public string orderNumber { get; set; }
        public string callBackUrl { get; set; }
        public string clientEmail { get; set; }
        public string clientName { get; set; }
        public string clientMobile { get; set; }
        public string note { get; set; }
        public object cancelUrl { get; set; }
        public List<Product> products { get; set; }
        public object currency { get; set; }
    }

    public class PaylinkRecieveDtos
    {
        public GatewayOrderRequest gatewayOrderRequest { get; set; }
        public double amount { get; set; }
        public string transactionNo { get; set; }
        public string orderStatus { get; set; }
        public object paymentErrors { get; set; }
        public string url { get; set; }
        public string qrUrl { get; set; }
        public string checkUrl { get; set; }
        public bool success { get; set; }
        public bool digitalOrder { get; set; }
        public object foreignCurrencyRate { get; set; }
    }
   
}
