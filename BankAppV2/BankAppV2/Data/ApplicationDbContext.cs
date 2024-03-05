using BankAppV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankAppV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Transaction> Transaction { get; set; } = default!;
        public DbSet<BankAppV2.Models.Account> Account { get; set; } = default!;
    }
}
