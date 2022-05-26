using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing;
using MqttLibrary.Examples.Pairing.Domain.Handlers;
using MqttLibrary.Examples.Pairing.Domain.Listeners;

namespace MqttLibrary.Examples.Pairing.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPairingDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMqttMessagingPipeline<LocalMqttMessagingClientOptions>(typeof(PairDeviceMessageHandler).Assembly);
        serviceCollection.AddMqttStartupListener<PairDeviceMqttStartupListener>();
        return serviceCollection;
    }
}