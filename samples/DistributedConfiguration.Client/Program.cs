using DistributedConfiguration.Client;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing;
using Mqtt.Library.RequestResponse;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
    {
        serviceCollection.AddLocalMqttMessagingClient(hostContext.Configuration);

        serviceCollection.AddLocalMqttMessageBus();

        serviceCollection.AddLocalMqttTopicClient();

        serviceCollection.AddMqttRequestClient<LocalMqttMessagingClientOptions>();

        serviceCollection.AddMqttMessagingPipeline<LocalMqttMessagingClientOptions>(typeof(ResponseHandler).Assembly);

        //serviceCollection.AddHostedService<BackgroundMqttCommandPublisher>();

        serviceCollection.AddHostedService<BackgroundRequestSender>();
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();

await host.RunAsync();