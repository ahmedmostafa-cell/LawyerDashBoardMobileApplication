using System.Collections.Generic;
using System;

namespace AlMohamyProject.Dtos
{
    public class PaymentStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ValidationErrors { get; set; }
        public Data Data { get; set; }
    }
    public class Data
    {
        public int InvoiceId { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceReference { get; set; }
        public string CustomerReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExpiryDate { get; set; }
        public string ExpiryTime { get; set; }
        public decimal InvoiceValue { get; set; }
        public object Comments { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string UserDefinedField { get; set; }
        public string InvoiceDisplayValue { get; set; }
        public decimal DueDeposit { get; set; }
        public string DepositStatus { get; set; }
        public List<object> InvoiceItems { get; set; }
        public List<object> InvoiceTransactions { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }

    public class Supplier
    {
        public int SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public decimal InvoiceShare { get; set; }
        public decimal ProposedShare { get; set; }
        public object DepositShare { get; set; }
    }
}
