using MessagingClient.Mqtt;
using MessagingLibrary.Mqtt.Local;
using Mqtt.Library.Client;
using Mqtt.Library.Processing;
using Mqtt.Library.Test;
using Mqtt.Library.Test.Client;
using Mqtt.Library.Test.Handlers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<BackgroundMqttPublisher>();

        //services.AddSingleton<HandlerFactory>(provider => provider.GetRequiredService);

        services.AddMessageProcessing();
                    
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