using DistributedConfiguration.Client;
using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
using DistributedConfiguration.Client.Listeners;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using MessagingLibrary.RequestResponse.Mqtt;
using Mqtt.Library.Client.Infrastructure;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        serviceCollection.AddInfrastructureMqttMessagingClient(hostContext.Configuration);

        serviceCollection.AddInfrastructureMqttMessageBus();

        serviceCollection.AddInfrastructureMqttTopicClient();

        serviceCollection.AddMqttRequestClient<InfrastructureMqttMessagingClientOptions>();

        serviceCollection.AddMessageHandler<UpdateLocalConfigurationMessageHandler>();
        serviceCollection.AddMessageHandler<NotifyUsersMessageHandler>();
        serviceCollection.AddMqttMessagingPipeline<InfrastructureMqttMessagingClientOptions>();
        serviceCollection.AddConsumerDefinitionProvider<PairedDevicesDefinitionProvider>();
        serviceCollection.AddConsumerDefinitionListenerProvider<ConsumerDefinitionListenerProvider>();
        serviceCollection.AddConsumerListener();

        serviceCollection.AddHostedService<BackgroundPublisher>();

        //serviceCollection.AddHostedService<BackgroundRequestSender>();
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<InfrastructureMqttMessagingClientOptions>();

await host.RunAsync();