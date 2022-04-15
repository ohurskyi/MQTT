using Mqtt.Library.Client;
using Mqtt.Library.MessageBus;
using Mqtt.Library.Processing;
using Mqtt.Library.Test;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Local;
using Mqtt.Library.TopicClient;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<BackgroundMqttPublisher>();
        
        services.AddMessageProcessing();
        services.AddMqttApplicationMessageReceivedHandler();
                    
        services.AddTransient<MessageHandlerTest>();
        services.AddTransient<MessageHandlerTest2>();
        
        services.AddMqttMessageBus<LocalMqttMessagingClientOptions>();
        services.AddMqttTopicClient<LocalMqttMessagingClientOptions>();

        services.AddMqttMessagingStartupServices();
        services.AddMqttMessagingClient<LocalMqttMessagingClientOptions>(hostContext.Configuration);
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();

await host.RunAsync();