using DistributedConfiguration.Domain.Handlers;
using DistributedConfiguration.Domain.Listeners;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPairingDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMessageHandlers(typeof(PairDeviceMessageHandler).Assembly);
        serviceCollection.AddMqttMessagingPipeline<InfrastructureMqttMessagingClientOptions>();
        serviceCollection.AddConsumerDefinitionListenerProvider<ConsumerDefinitionListenerProvider>();
        serviceCollection.AddConsumerListener();
        return serviceCollection;
    }
}