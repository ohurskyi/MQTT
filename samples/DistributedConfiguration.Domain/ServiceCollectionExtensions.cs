using DistributedConfiguration.Domain.Handlers;
using DistributedConfiguration.Domain.Listeners;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Local;

namespace DistributedConfiguration.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPairingDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMqttMessagingPipeline<InfrastructureMqttMessagingClientOptions>(typeof(PairDeviceMessageHandler).Assembly);
        serviceCollection.AddMqttStartupListener<DistributedConfigurationMqttStartupListener>();
        return serviceCollection;
    }
}