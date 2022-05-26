using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing;
using MqttLibrary.Examples.Pairing.Domain;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        var configuration = hostContext.Configuration;
        
        serviceCollection.AddLocalMqttMessagingClient(configuration);

        serviceCollection.AddLocalMqttTopicClient();

        serviceCollection.AddPairingDomainServices();

    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();

await host.RunAsync();