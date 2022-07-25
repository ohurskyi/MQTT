using DistributedConfiguration.Client;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Mqtt.Library.Client.Infrastructure;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        serviceCollection.AddInfrastructureMessagingClient(hostContext.Configuration);

        serviceCollection.AddInfrastructureMessageBus();

        serviceCollection.AddInfrastructureTopicClient();

        serviceCollection.AddInfrastructureRequestClient();

        serviceCollection.AddInfrastructureMqttPipe();

        serviceCollection.AddDomainServices();

        serviceCollection.AddHostedService<BackgroundPublisher>();

        serviceCollection.AddHostedService<BackgroundRequestSender>();
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<InfrastructureClientOptions>();

await host.RunAsync();