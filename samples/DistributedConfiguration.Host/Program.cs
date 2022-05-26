using Mqtt.Library.Client.Local;
using MqttLibrary.Examples.Pairing.Domain;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        var configuration = hostContext.Configuration;
        
        serviceCollection.AddLocalMqttMessagingClient(configuration);

        serviceCollection.AddLocalMqttTopicClient();

        serviceCollection.AddPairingDomainServices();

    })
    .Build();

await host.RunAsync();