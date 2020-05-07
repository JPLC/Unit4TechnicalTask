using InvoiceManager.App.Services.Models;
using System.Collections.Generic;

namespace InvoiceManager.App.Models
{
    public class InvoiceViewModel
    {
        public InvoiceDetailsVM Invoices { get; set; }

        public IEnumerable<CurrencyCode> CurrencyCodes { get; set; }

        public string SelectedCode { get; set; }
    }
}