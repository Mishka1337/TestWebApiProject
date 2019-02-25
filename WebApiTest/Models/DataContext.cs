using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApiTest.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cardholder> CardHolders { get; set; }
        public DbSet<Atm> Atms { get; set; }

        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
           
        }
    }
}
