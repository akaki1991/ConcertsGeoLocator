using ConcertsGeoLocator.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConcertsGeoLocator;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new DefaultDependencyResolver().RegisterServices()
            .BuildServiceProvider();

        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File(@"C:\ConcertsGeoLocator\concerts-geo-locator-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

        var eventProcceror = serviceProvider.GetService<IEventProcessor>();

        bool continueRunning = true;

        while (continueRunning)
        {
            Console.WriteLine("Press 'r' to run or 'e' to exit.");

            var userInput = Console.ReadKey();

            switch (userInput.Key)
            {
                case ConsoleKey.R:
                    await eventProcceror.ProcessEventsAsync();
                    break;
                case ConsoleKey.E:
                    continueRunning = false;
                    break;
                default:
                    break;
            }
        }
    }
}