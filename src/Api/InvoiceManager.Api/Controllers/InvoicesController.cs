using InvoiceManager.Api.Configuration;
using InvoiceManager.Services.Abstractions;
using InvoiceManager.Services.Contracts.Invoices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManager.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class InvoicesController : BaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IExchangeRateService _exchangeRateService;
        public InvoicesController(IInvoiceService invoiceService, IExchangeRateService exchangeRateService)
        {
            _invoiceService = invoiceService;
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("exchangeRates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetExchangeRate(string currency)
        {
            return Resolve(_exchangeRateService.ImportRates(currency));
        }

        [HttpGet("Invoices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInvoices(string currency, CancellationToken cancellationToken)
        {
            return Resolve(await _invoiceService.GetAllInvoices(currency, cancellationToken));
        }

        [HttpGet("Invoices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoice(Guid id, string currency, CancellationToken cancellationToken)
        {
            return Resolve(await _invoiceService.GetInvoiceById(id, currency, cancellationToken));
        }

        [HttpPost("Invoices")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceCreate invoiceCreate, CancellationToken cancellationToken)
        {
            return Resolve(await _invoiceService.AddInvoice(invoiceCreate, cancellationToken), HttpStatusCode.Created);
        }

        [HttpPut("Invoices")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInvoice([FromBody] InvoiceUpdate invoiceUpdate, CancellationToken cancellationToken)
        {
            return Resolve(await _invoiceService.UpdateInvoice(invoiceUpdate, cancellationToken), HttpStatusCode.Created);
        }

        [HttpDelete("Invoices/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteInvoice(Guid id, CancellationToken cancellationToken)
        {
            return Resolve(await _invoiceService.DeleteInvoice(id, cancellationToken));
        }
    }
}
