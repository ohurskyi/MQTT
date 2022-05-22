using Mqtt.Library.Client;
using Mqtt.Library.MessageBus;
using Mqtt.Library.Processing;
using Mqtt.Library.Test;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Listeners;
using Mqtt.Library.TopicClient;
using MqttLibrary.Examples.Domain.Handlers;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        MqttPipelineDeviceHandlersLocal(services, hostContext.Configuration);
        MqttPipelineDeviceHandlersTest(services, hostContext.Configuration);
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();
host.Services.UseMqttMessageReceivedHandler<TestMqttMessagingClientOptions>();

await host.RunAsync();

void MqttPipelineDeviceHandlersLocal(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddMqttMessagingClient<LocalMqttMessagingClientOptions>(configuration);

    serviceCollection.AddMqttMessagingPipeline<LocalMqttMessagingClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);

    serviceCollection.AddHostedService<BackgroundLocalMqttPublisher>();
    
    serviceCollection.AddMqttMessageBus<LocalMqttMessagingClientOptions>();
    
    serviceCollection.AddMqttTopicClient<LocalMqttMessagingClientOptions>();

    serviceCollection.AddMqttStartupListener<DeviceBaseMqttStartupListener>();
}

void MqttPipelineDeviceHandlersTest(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddMqttMessagingClient<TestMqttMessagingClientOptions>(configuration);

    serviceCollection.AddMqttMessagingPipeline<TestMqttMessagingClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);

    serviceCollection.AddHostedService<BackgroundLocalMqttPublisher>();
    
    serviceCollection.AddMqttMessageBus<TestMqttMessagingClientOptions>();
    
    serviceCollection.AddMqttTopicClient<TestMqttMessagingClientOptions>();

    serviceCollection.AddMqttStartupListener<DeviceBaseMqttStartupListenerDevConfig>();
}