using Mqtt.Library.Client;
using Mqtt.Library.MessageBus;
using Mqtt.Library.Processing;
using Mqtt.Library.Test;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Local;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<BackgroundMqttPublisher>();

        //services.AddSingleton<HandlerFactory>(provider => provider.GetRequiredService);

        services.AddMessageProcessing();
        services.AddMqttApplicationMessageReceivedHandler();
                    
        services.AddTransient<MessageHandlerTest>();
        services.AddTransient<MessageHandlerTest2>();
        
        services.AddMqttMessageBus<LocalMqttMessagingClientOptions>();
        services.AddTopicClient<LocalMqttMessagingClientOptions>();

        services.AddMqttMessagingStartupServices();
        services.AddMqttMessagingClient<LocalMqttMessagingClientOptions>(hostContext.Configuration);
        
    })
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();

await host.RunAsync();