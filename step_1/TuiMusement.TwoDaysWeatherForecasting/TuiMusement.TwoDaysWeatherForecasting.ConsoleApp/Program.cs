using Microsoft.Extensions.Hosting;

using var host = BuildHost();

IHost BuildHost() => Host
    .CreateDefaultBuilder(args)
    .Build();

Console.WriteLine("Hello, World!");