using ConcertsGeoLocator.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertsGeoLocator;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new DefaultDependencyResolver().RegisterServices()
            .BuildServiceProvider();

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