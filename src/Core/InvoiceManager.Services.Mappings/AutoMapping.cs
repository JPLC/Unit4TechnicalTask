using InvoiceManager.Domain.Entities;
using InvoiceManager.Services.Contracts.Invoices;

namespace InvoiceManager.Services.Mappings
{
    using AutoMapper;
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Invoice, InvoiceDetails>().ReverseMap();
            CreateMap<InvoiceCreate, Invoice>();
            CreateMap<InvoiceUpdate, Invoice>();
        }
    }
}
