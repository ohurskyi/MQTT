using DistributedConfiguration.Client;
using Mqtt.Library.Client.Local;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        serviceCollection.AddLocalMqttMessagingClient(hostContext.Configuration);

        serviceCollection.AddLocalMqttMessageBus();

        serviceCollection.AddHostedService<BackgroundMqttCommandPublisher>();
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

await host.RunAsync();