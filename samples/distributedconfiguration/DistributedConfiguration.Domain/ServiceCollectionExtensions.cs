using DistributedConfiguration.Domain.Handlers;
using DistributedConfiguration.Domain.Listeners;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using MessagingLibrary.TopicClient.Mqtt;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPairingDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMqttMessagingPipeline<InfrastructureMqttMessagingClientOptions>(typeof(PairDeviceMessageHandler).Assembly);
        serviceCollection.AddConsumerDefinitionProvider<InfrastructureMqttMessagingClientOptions, ThisDomainConsumerDefinitionProvider>();
        serviceCollection.AddConsumerListener<InfrastructureMqttMessagingClientOptions>();
        return serviceCollection;
    }
}