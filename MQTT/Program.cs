using Mqtt.Library.Client;
using Mqtt.Library.MessageBus;
using Mqtt.Library.Processing;
using Mqtt.Library.Test;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Listeners;
using Mqtt.Library.TopicClient;
using MqttLibrary.Examples.Domain.Handlers;
using Mqtt.Library.Client.Local;
using MqttLibrary.Examples.Domain;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        MqttPipelineDeviceHandlersLocal(services, hostContext.Configuration);
        //MqttPipelineDeviceHandlersTest(services, hostContext.Configuration);
    })
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console())
    .Build();

host.Services.UseMqttMessageReceivedHandler<LocalMqttMessagingClientOptions>();
//host.Services.UseMqttMessageReceivedHandler<TestMqttMessagingClientOptions>();

await host.RunAsync();

void MqttPipelineDeviceHandlersLocal(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddLocalMqttMessagingClient(configuration);
    
    serviceCollection.AddLocalMqttMessageBus();

    serviceCollection.AddLocalMqttTopicClient();

    serviceCollection.AddDeviceDomainServices();

    serviceCollection.AddHostedService<BackgroundLocalMqttPublisher>();
}

void MqttPipelineDeviceHandlersTest(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddMqttMessagingClient<TestMqttMessagingClientOptions>(configuration);

    serviceCollection.AddHostedService<BackgroundLocalMqttPublisher>();
    
    serviceCollection.AddMqttMessageBus<TestMqttMessagingClientOptions>();
    
    serviceCollection.AddMqttTopicClient<TestMqttMessagingClientOptions>();
    
    //serviceCollection.AddMqttMessagingPipeline<TestMqttMessagingClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);

    //serviceCollection.AddMqttStartupListener<DeviceBaseMqttStartupListenerDevConfig>();
}