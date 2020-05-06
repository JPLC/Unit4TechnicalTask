using AutoMapper;
using InvoiceManager.Domain.Entities;
using InvoiceManager.Services.Abstractions;
using InvoiceManager.Services.Contracts.ExchangeRate;
using InvoiceManager.Services.Contracts.Invoices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Toolkit.Services;
using Toolkit.UoW.Abstractions;

namespace InvoiceManager.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExchangeRateService _exchangeRateService;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper, IExchangeRateService exchangeRateService)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.SetLazyLoading(false);
            _mapper = mapper;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<OperationResult<IEnumerable<InvoiceDetails>>> GetAllInvoices(string currency, CancellationToken cancellationToken)
        {
            var dbData = _unitOfWork.GetRepository<Invoice>().FindAll();
            var rets = _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDetails>>(dbData);
            if (!string.IsNullOrEmpty(currency))
            {
                var rates = _exchangeRateService.ImportRates(currency);
                foreach (var ret in rets)
                {
                    DefineRates(ret, rates, currency);
                }
            }
            return new OperationResult<IEnumerable<InvoiceDetails>>(rets);
        }

        public async Task<OperationResult<InvoiceDetails>> GetInvoiceById(Guid id, string currency, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.GetRepository<Invoice>().Find(f => f.InvoiceId == id);
            if (result == null)
                return new OperationResult<InvoiceDetails> { NotFound = true };
            var ret = _mapper.Map<Invoice, InvoiceDetails>(result);
            if (!string.IsNullOrEmpty(currency))
            {
                var rates = _exchangeRateService.ImportRates(currency);
                DefineRates(ret, rates, currency);
            }
            return new OperationResult<InvoiceDetails>(ret);
        }

        public async Task<OperationResult<InvoiceDetails>> AddInvoice(InvoiceCreate invoiceToCreate, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceDetails>();
            try
            {
                _unitOfWork.BeginTransaction();
                var createInvoice = _mapper.Map<InvoiceCreate, Invoice>(invoiceToCreate);
                var invoiceAdded = _unitOfWork.GetRepository<Invoice>().Add(createInvoice);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                result.Entity = _mapper.Map<Invoice, InvoiceDetails>(invoiceAdded);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                result.Errors.Add("ServerErrorMessage", "Server error occurred");
                throw;
            }
            return result;
        }

        public async Task<OperationResult<InvoiceDetails>> UpdateInvoice(InvoiceUpdate invoiceUpdate, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceDetails>();
            var invoice = _unitOfWork.GetRepository<Invoice>().Find(t => t.InvoiceId == invoiceUpdate.InvoiceId);
            if (invoice == null)
            {
                result.NotFound = true;
                return result;
            }
            try
            {
                _unitOfWork.BeginTransaction();
                var updatedInvoice = _mapper.Map<InvoiceUpdate, Invoice>(invoiceUpdate);
                _unitOfWork.GetRepository<Invoice>().Update(updatedInvoice);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                result.Entity = _mapper.Map<Invoice, InvoiceDetails>(updatedInvoice);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                result.Errors.Add("ServerErrorMessage", "Server error occurred");
                throw;
            }
            return result;
        }

        public async Task<OperationResult<InvoiceDetails>> DeleteInvoice(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceDetails>();
            var invoice = _unitOfWork.GetRepository<Invoice>().Find(t => t.InvoiceId == id);
            if (invoice == null)
            {
                result.NotFound = true;
                return result;
            }
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.GetRepository<Invoice>().Remove(t => t.InvoiceId == id);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                result.Entity = _mapper.Map<Invoice, InvoiceDetails>(invoice);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                result.Errors.Add("ServerErrorMessage", "Server error occurred");
                throw;
            }
            return result;
        }

        private void DefineRates(InvoiceDetails ret, OperationResult<ExchangeRateResult> rates, string currency)
        {
            var rate = rates.Entity.ConversionRates[ret.Currency.ToUpperInvariant()];
            ret.Amount /= (decimal)rate;
            ret.Currency = currency.ToUpperInvariant();
        }
    }
}
