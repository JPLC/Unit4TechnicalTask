using InvoiceManager.App.Services.Abstractions;
using InvoiceManager.App.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Toolkit.Api;
using Toolkit.Services;

namespace InvoiceManager.App.Services
{
    public class InvoiceManagerAPIService : IInvoiceManagerApiService
    {
        private readonly ApiClient _apiClient;
        private readonly string _url;

        public InvoiceManagerAPIService(ApiClient apiClient)
        {
            _apiClient = apiClient;
            _url = "invoices/";
        }

        public async Task<OperationResult<IEnumerable<InvoiceDetailsVM>>> GetAllInvoices(string currency, CancellationToken cancellationToken)
        {
            var requestUrl = string.IsNullOrEmpty(currency) ? _url : $"{_url}?currency={currency}";
            var result = await _apiClient.GetAsync<OperationResult<IEnumerable<InvoiceDetailsVM>>>(requestUrl);
            return result;
        }

        public async Task<OperationResult<InvoiceDetailsVM>> GetInvoiceById(Guid id, string currency, CancellationToken cancellationToken)
        {
            var requestUrl = string.IsNullOrEmpty(currency) ? $"{_url}{id}" : $"{_url}{id}?currency={currency}";
            var result = await _apiClient.GetAsync<OperationResult<InvoiceDetailsVM>>(requestUrl);
            return result;
        }

        public async Task<OperationResult<InvoiceDetailsVM>> AddInvoice(InvoiceCreateDto invoice, CancellationToken cancellationToken)
        {
            var requestUrl = $"{_url}";
            var result = await _apiClient.PostAsync<InvoiceCreateDto, OperationResult<InvoiceDetailsVM>>(requestUrl, invoice);
            return result;
        }

        public async Task<OperationResult<InvoiceDetailsVM>> UpdateInvoice(InvoiceUpdateVM invoice, CancellationToken cancellationToken)
        {
            var requestUrl = $"{_url}{invoice.InvoiceId}";
            var result = await _apiClient.PutAsync<InvoiceUpdateVM, OperationResult<InvoiceDetailsVM>>(requestUrl, invoice);
            return result;
        }

        public async Task<OperationResult<InvoiceDetailsVM>> DeleteInvoice(Guid id, CancellationToken cancellationToken)
        {
            var requestUrl = $"{_url}{id}";
            var result = await _apiClient.DeleteAsync<OperationResult<InvoiceDetailsVM>>(requestUrl);
            return result;
        }
    }
}
