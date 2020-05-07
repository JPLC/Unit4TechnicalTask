using InvoiceManager.App.Models;
using InvoiceManager.App.Services.Abstractions;
using InvoiceManager.App.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManager.App.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceManagerApiService _invoiceManagerApi;

        public InvoiceController(IInvoiceManagerApiService invoiceManagerApi)
        {
            _invoiceManagerApi = invoiceManagerApi;
        }

        // GET: Invoice
        public async Task<IActionResult> Index(InvoicesViewModel vm1)
        {
            var currency = !string.IsNullOrEmpty(vm1.SelectedCode) ? vm1.SelectedCode : string.Empty;
            var result = await _invoiceManagerApi.GetAllInvoices(currency, new CancellationToken());
            var currencyCodes = ModelUtils.GetCurrencyCodes().Select(a => new SelectListItem()
            {
                Text = a.Value,
                Value = a.Code,
                Selected = false
            });
            var vm = new InvoicesViewModel
            {
                Invoices = result.Entity,
                CurrencyCodes = currencyCodes,
                SelectedCode = !string.IsNullOrEmpty(vm1.SelectedCode) ? vm1.SelectedCode : string.Empty
            };
            return View(vm);
        }


        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var result = await _invoiceManagerApi.GetInvoiceById(id.Value, null, new CancellationToken());
            if (result == null) return NotFound();
            return View(result.Entity);
        }

        // GET: Invoice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Supplier,DateIssued,Currency,Amount,Description")] InvoiceCreateDto invoiceCreateDto)
        {
            if (!ModelState.IsValid) return View(invoiceCreateDto);
            var result = await _invoiceManagerApi.AddInvoice(invoiceCreateDto, new CancellationToken());
            if (!result.Success) return View("InvoiceError");
            return RedirectToAction(nameof(Index));
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var result = await _invoiceManagerApi.GetInvoiceById(id.Value, null, new CancellationToken());
            if (result.NotFound) return NotFound();
            return View(new InvoiceUpdateVM
            {
                Amount = result.Entity.Amount,
                Currency = result.Entity.Currency,
                DateIssued = result.Entity.DateIssued,
                Description = result.Entity.Description,
                InvoiceId = result.Entity.InvoiceId,
                Supplier = result.Entity.Supplier,
            });
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceId,Supplier,DateIssued,Currency,Amount,Description")] InvoiceUpdateVM invoiceUpdateVM)
        {
            if (id != invoiceUpdateVM.InvoiceId) return NotFound();
            if (!ModelState.IsValid) return View(invoiceUpdateVM);
            var result = await _invoiceManagerApi.UpdateInvoice(invoiceUpdateVM, new CancellationToken());
            if (result.NotFound) return NotFound();
            if (!result.Success) return View("InvoiceError");
            return RedirectToAction(nameof(Index));
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var result = await _invoiceManagerApi.GetInvoiceById(id.Value, null, new CancellationToken());
            if (result.NotFound) return NotFound();
            return View(result.Entity);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _invoiceManagerApi.DeleteInvoice(id, new CancellationToken());
            if (result.NotFound) { return NotFound(); }
            if (!result.Success) return View("InvoiceError");
            return RedirectToAction(nameof(Index));
        }
    }
}
