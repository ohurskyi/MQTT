using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing;
using MqttLibrary.Examples.Domain.Handlers;
using MqttLibrary.Examples.Domain.Listeners;

namespace MqttLibrary.Examples.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeviceDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMqttMessagingPipeline<LocalMqttMessagingClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);
        serviceCollection.AddMqttStartupListener<DeviceBaseMqttStartupListener>();
        return serviceCollection;
    }
}