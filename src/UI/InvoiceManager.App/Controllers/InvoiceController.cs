using InvoiceManager.App.Services.Abstractions;
using InvoiceManager.App.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManager.App.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceManagerApiService _invoiceManagerApi;

        public InvoiceController(IInvoiceManagerApiService invoiceManagerApi)
        {
            _invoiceManagerApi = invoiceManagerApi;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var result = await _invoiceManagerApi.GetAllInvoices(null, new CancellationToken());
            return View(result.Entity);
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
        public async Task<IActionResult> Create([Bind("InvoiceId,Supplier,DateIssued,Currency,Amount,Description")] InvoiceCreateVM invoiceCreateVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _invoiceManagerApi.AddInvoice(invoiceCreateVM, new CancellationToken());
                if (result.Success)
                    return RedirectToAction(nameof(Index));
                return View(invoiceCreateVM);
            }
            return View(invoiceCreateVM);
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
            if (ModelState.IsValid)
            {
                var result = await _invoiceManagerApi.UpdateInvoice(invoiceUpdateVM, new CancellationToken());
                if (result.NotFound) return NotFound();
                if (result.Success) return RedirectToAction(nameof(Index));
                return View(invoiceUpdateVM);
            }
            return View(invoiceUpdateVM);
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
            if (!result.Success) return RedirectToAction(nameof(Delete), id);
            return RedirectToAction(nameof(Index));
        }
    }
}
