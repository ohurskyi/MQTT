using MessagingClient.Mqtt;
using MessagingLibrary.Mqtt.Local;
using Mqtt.Library.Client;
using Mqtt.Library.Test;
using Mqtt.Library.Test.Client;
using Mqtt.Library.Test.Core;
using Mqtt.Library.Test.Handlers;
using MqttClientTest;
using MqttClientTest.Messaging.Processing;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<BackgroundMqttPublisher>();

        services.AddSingleton<HandlerFactory>(provider => provider.GetRequiredService);
        services.AddSingleton<IMqttMessageExecutor, ScopedMessageExecutor>();
        services.AddSingleton<IMessageHandlerFactory, MessageHandlerFactory>();
        
        services.AddScoped<MessageHandlerWrapper>();
                    
        services.AddTransient<MessageHandlerTest>();
        services.AddTransient<MessageHandlerTest2>();

        services.AddMqttMessagingStartupServices();
        services.AddMqttApplicationMessageReceivedHandler();
        services.AddMqttMessageBus<LocalMqttMessagingClientOptions>();
        services.AddTopicClient<LocalMqttMessagingClientOptions>();
        services.AddMqttMessagingClient<LocalMqttMessagingClientOptions>(hostContext.Configuration);
        
    })
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();

await host.RunAsync();