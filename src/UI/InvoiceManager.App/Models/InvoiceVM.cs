using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.App.Models
{
    public class InvoiceVM
    {
        [Key]
        public Guid InvoiceId { get; set; }

        [MaxLength(100)]
        public string Supplier { get; set; }

        public DateTime DateIssued { get; set; }

        [StringLength(3)]
        public string Currency { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
