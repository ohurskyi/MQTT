using DistributedConfiguration.Client;
using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
using DistributedConfiguration.Client.Listeners;
using MessagingLibrary.Processing.Mqtt;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Mqtt.Library.Client.Local;
using Mqtt.Library.RequestResponse;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        serviceCollection.AddLocalMqttMessagingClient(hostContext.Configuration);

        serviceCollection.AddLocalMqttMessageBus();

        serviceCollection.AddLocalMqttTopicClient();

        serviceCollection.AddMqttRequestClient<LocalMqttMessagingClientOptions>();

        serviceCollection.AddMqttMessagingPipeline<LocalMqttMessagingClientOptions>(typeof(UpdateLocalConfigurationMessageHandler).Assembly);

        serviceCollection.AddHostedService<BackgroundPublisher>();

        serviceCollection.AddMqttStartupListener<PairedDevicesConfigurationChangedMqttStartupListener>();

        //serviceCollection.AddHostedService<BackgroundRequestSender>();
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();

await host.RunAsync();