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
            CreateMap<InvoiceCreate, Invoice>()
                .ForMember(t => t.Currency, x => x.MapFrom(t => t.Currency.ToUpperInvariant()));
            CreateMap<InvoiceUpdate, Invoice>()
                .ForMember(t => t.Currency, x => x.MapFrom(t => t.Currency.ToUpperInvariant()));
        }
    }
}
