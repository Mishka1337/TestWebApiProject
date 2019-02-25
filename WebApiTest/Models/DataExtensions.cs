using System.Linq;

namespace WebApiTest.Models
{
    public static class DataExtensions
    {
        public static void EnsureSeedData(this DataContext context)
        {
            if (!context.CardHolders.Any())
            {
                context.CardHolders.Add(new Cardholder { Count100 = 1, Count200 = 2, Count1000 = 0, Count5000 = 0 });
                context.CardHolders.Add(new Cardholder { Count100 = 5, Count200 = 42, Count1000 = 200, Count5000 = 228 });
            }
            
            if (!context.Atms.Any())
            {
                context.Atms.Add(new Atm {Count100 = 1000, Count200 = 1000, Count1000 = 1000, Count5000 = 1000});
                context.Atms.Add(new Atm {Count100 =  1,Count200 = 22,Count1000 = 23,Count5000 = 2});
            }

            if (context.Accounts.Any())
            {
                context.SaveChanges();
                return;
            }
            context.Accounts.Add(new Account { CardholderId = 1, Amount = 2228 });
            context.Accounts.Add(new Account { Amount = 0, CardholderId = 2 });
            context.SaveChanges();
        }
    }
}