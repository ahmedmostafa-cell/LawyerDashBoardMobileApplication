namespace AlMohamyProject.Dtos
{
    public class InvoiceResponseData
    {
        public int InvoiceId { get; set; }
        public bool IsDirectPayment { get; set; }
        public string PaymentURL { get; set; }
        public object CustomerReference { get; set; }
        public object UserDefinedField { get; set; }
        public string RecurringId { get; set; }
    }

    public class InvoiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ValidationErrors { get; set; }
        public InvoiceResponseData Data { get; set; }
    }

}
