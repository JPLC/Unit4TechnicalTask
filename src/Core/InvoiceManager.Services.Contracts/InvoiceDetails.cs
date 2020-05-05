using System;

namespace InvoiceManager.Services.Contracts
{
    public class InvoiceDetails
    {
        public Guid InvoiceId { get; set; }

        public string Supplier { get; set; }

        public DateTime DateIssued { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
