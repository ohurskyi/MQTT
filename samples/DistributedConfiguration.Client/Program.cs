using DistributedConfiguration.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<BackgroundMqttCommandPublisher>(); })
    .Build();

await host.RunAsync();