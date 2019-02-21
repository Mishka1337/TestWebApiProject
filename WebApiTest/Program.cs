using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WebApiTest.Models;

namespace WebApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Atm.Count100 = 10000;
            Atm.Count200 = 10000;
            Atm.Count1000 = 10000;
            Atm.Count5000 = 10000;
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
