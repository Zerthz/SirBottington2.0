using Microsoft.Extensions.Hosting;

namespace SirBottington
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = DI.CreateHostBuilder(args).Build();

            using (host)
            {
               await host.RunAsync();
            }

        }
    }
}