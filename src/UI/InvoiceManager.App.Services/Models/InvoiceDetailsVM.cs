using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.App.Services.Models
{
    public class InvoiceDetailsVM
    {
        [Required]
        public Guid InvoiceId { get; set; }

        public string Supplier { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
