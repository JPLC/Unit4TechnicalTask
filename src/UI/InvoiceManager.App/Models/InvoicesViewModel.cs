using InvoiceManager.App.Services.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InvoiceManager.App.Models
{
    public class InvoicesViewModel
    {
        public IEnumerable<InvoiceDetailsVM> Invoices { get; set; }

        public IEnumerable<SelectListItem> CurrencyCodes { get; set; }

        public string SelectedCode { get; set; }
    }
}