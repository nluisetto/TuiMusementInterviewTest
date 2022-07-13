using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Common;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.ProgressNotifier;

public class ConsoleProgressNotifier : IProgressNotifier
{
    public void Notify(string progress)
    {
        Console.WriteLine(progress);
    }
}
