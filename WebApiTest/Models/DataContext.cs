using Microsoft.EntityFrameworkCore;

namespace WebApiTest.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cardholder> CardHolders { get; set; }

        public DataContext(DbContextOptions<DataContext> options):
            base(options)
        { }
    }
}
