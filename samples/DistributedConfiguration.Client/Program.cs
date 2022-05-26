using DistributedConfiguration.Client;
using Mqtt.Library.Client.Local;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        serviceCollection.AddLocalMqttMessagingClient(hostContext.Configuration);

        serviceCollection.AddLocalMqttMessageBus();

        serviceCollection.AddHostedService<BackgroundMqttCommandPublisher>();
    })
    .Build();

await host.RunAsync();