using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Services.Contracts.Invoices
{
    public class InvoiceUpdate
    {
        [Required]
        public Guid InvoiceId { get; set; }

        [MaxLength(100)]
        public string Supplier { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
