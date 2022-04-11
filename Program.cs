using Mqtt.Library.Test;
using Mqtt.Library.Test.Core;
using Mqtt.Library.Test.Handlers;
using MqttClientTest;
using MqttClientTest.Messaging.Processing;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<BackgroundMqttPublisher>();

        services.AddSingleton<HandlerFactory>(provider => provider.GetRequiredService);
        services.AddSingleton<IMqttMessageExecutor, MqttMessageExecutor>();
        services.AddSingleton<MessageHandlerWrapper>();
        services.AddSingleton<IMessageHandlerFactory, MessageHandlerFactory>();
                    
        services.AddTransient<MessageHandlerTest>();
        services.AddTransient<MessageHandlerTest2>();
        
    })
    .Build();

await host.RunAsync();