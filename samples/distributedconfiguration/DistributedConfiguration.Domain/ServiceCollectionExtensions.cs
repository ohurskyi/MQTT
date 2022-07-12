using DistributedConfiguration.Domain.Handlers;
using DistributedConfiguration.Domain.Listeners;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using MessagingLibrary.TopicClient.Mqtt;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPairingDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMqttMessagingPipeline<InfrastructureMqttMessagingClientOptions>(typeof(PairDeviceMessageHandler).Assembly);
        serviceCollection.AddConsumerDefinitionProvider<PairingConsumerDefinitionProvider>();
        serviceCollection.AddSingleton<IConsumerDefinitionListenerProvider, ConsumerDefinitionListenerProvider>();
        serviceCollection.AddConsumerListener();
        return serviceCollection;
    }
}