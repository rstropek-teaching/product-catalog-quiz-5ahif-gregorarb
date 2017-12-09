using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProductCatalog
{
    public class Program
    {
        // Tutorial: https://restdb.io/docs/quick-start
        // Database: https://product-b1ac.restdb.io/home/

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
