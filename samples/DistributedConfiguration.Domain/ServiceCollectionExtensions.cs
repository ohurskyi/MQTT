using DistributedConfiguration.Domain.Handlers;
using DistributedConfiguration.Domain.Listeners;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing;

namespace DistributedConfiguration.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPairingDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMqttMessagingPipeline<LocalMqttMessagingClientOptions>(typeof(PairDeviceMessageHandler).Assembly);
        serviceCollection.AddMqttStartupListener<DistributedConfigurationMqttStartupListener>();
        return serviceCollection;
    }
}