using InvoiceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Domain
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoice { get; set; }

    }
}