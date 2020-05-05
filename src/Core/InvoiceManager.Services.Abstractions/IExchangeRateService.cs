using InvoiceManager.Services.Contracts.ExchangeRate;
using Toolkit.Services;

namespace InvoiceManager.Services.Abstractions
{
    public interface IExchangeRateService
    {
        OperationResult<ExchangeRateResult> ImportRates(string currency);
    }
}