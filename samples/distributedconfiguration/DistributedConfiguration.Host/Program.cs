using DistributedConfiguration.Domain;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Mqtt.Library.Client.Infrastructure;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        var configuration = hostContext.Configuration;
        
        serviceCollection.AddInfrastructureMqttMessagingClient(configuration);

        serviceCollection.AddInfrastructureMqttTopicClient();

        serviceCollection.AddInfrastructureMqttMessageBus();

        serviceCollection.AddInfrastructureMqttPipe();

        serviceCollection.AddPairingDomainServices();

    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<InfrastructureClientOptions>();

await host.RunAsync();