using InvoiceManager.App.Models;
using InvoiceManager.Domain;
using InvoiceManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.App.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceDbContext _context;
        public InvoiceController(InvoiceDbContext context)
        {
            //TODO substituir o context por uma camada de servicos de chamadas a api
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var result = await _context.Invoice.ToListAsync();
            return View(result.Select(invoice => new InvoiceVM
            {

                InvoiceId = invoice.InvoiceId,
                Amount = invoice.Amount,
                Currency = invoice.Currency,
                DateIssued = invoice.DateIssued,
                Description = invoice.Description,
                Supplier = invoice.Supplier
            }
            ));
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(new InvoiceVM
            {
                InvoiceId = invoice.InvoiceId,
                Amount = invoice.Amount,
                Currency = invoice.Currency,
                DateIssued = invoice.DateIssued,
                Description = invoice.Description,
                Supplier = invoice.Supplier
            });
        }

        // GET: Invoice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,Supplier,DateIssued,Currency,Amount,Description")] InvoiceVM invoiceVM)
        {
            if (ModelState.IsValid)
            {
                invoiceVM.InvoiceId = Guid.NewGuid();
                _context.Add(new Invoice
                {
                    InvoiceId = invoiceVM.InvoiceId,
                    Amount = invoiceVM.Amount,
                    Currency = invoiceVM.Currency,
                    DateIssued = invoiceVM.DateIssued,
                    Description = invoiceVM.Description,
                    Supplier = invoiceVM.Supplier
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceVM);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(new InvoiceVM
            {
                InvoiceId = invoice.InvoiceId,
                Amount = invoice.Amount,
                Currency = invoice.Currency,
                DateIssued = invoice.DateIssued,
                Description = invoice.Description,
                Supplier = invoice.Supplier
            });
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceId,Supplier,DateIssued,Currency,Amount,Description")] InvoiceVM invoiceVM)
        {
            if (id != invoiceVM.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Invoice
                    {
                        InvoiceId = invoiceVM.InvoiceId,
                        Amount = invoiceVM.Amount,
                        Currency = invoiceVM.Currency,
                        DateIssued = invoiceVM.DateIssued,
                        Description = invoiceVM.Description,
                        Supplier = invoiceVM.Supplier
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceVMExists(invoiceVM.InvoiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceVM);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(new InvoiceVM
            {
                InvoiceId = invoice.InvoiceId,
                Amount = invoice.Amount,
                Currency = invoice.Currency,
                DateIssued = invoice.DateIssued,
                Description = invoice.Description,
                Supplier = invoice.Supplier
            });
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceVMExists(Guid id)
        {
            return _context.Invoice.Any(e => e.InvoiceId == id);
        }
    }
}
