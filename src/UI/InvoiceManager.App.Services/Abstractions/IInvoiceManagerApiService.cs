using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InvoiceManager.App.Services.Models;
using Toolkit.Services;

namespace InvoiceManager.App.Services.Abstractions
{
    public interface IInvoiceManagerApiService
    {
        Task<OperationResult<IEnumerable<InvoiceDetailsVM>>> GetAllInvoices(string currency, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetailsVM>> GetInvoiceById(Guid id, string currency, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetailsVM>> AddInvoice(InvoiceCreateVM invoice, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetailsVM>> UpdateInvoice(InvoiceUpdateVM invoice, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetailsVM>> DeleteInvoice(Guid id, CancellationToken cancellationToken);
    }
}
