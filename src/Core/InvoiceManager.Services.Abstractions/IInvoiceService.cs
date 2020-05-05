using InvoiceManager.Services.Contracts.Invoices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Toolkit.Services;

namespace InvoiceManager.Services.Abstractions
{
    public interface IInvoiceService
    {
        Task<OperationResult<IEnumerable<InvoiceDetails>>> GetAllInvoices(string currency, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetails>> GetInvoiceById(Guid id, string currency, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetails>> AddInvoice(InvoiceCreate invoice, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetails>> UpdateInvoice(InvoiceUpdate invoice, CancellationToken cancellationToken);

        Task<OperationResult<InvoiceDetails>> DeleteInvoice(Guid id, CancellationToken cancellationToken);
    }
}
