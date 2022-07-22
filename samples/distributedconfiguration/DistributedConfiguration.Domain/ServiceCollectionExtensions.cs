using DistributedConfiguration.Domain.Consumers;
using DistributedConfiguration.Domain.Handlers;
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
        serviceCollection.AddConsumerDefinitionListenerProvider<ConsumerDefinitionListenerProvider>();
        
        serviceCollection.AddMqttPipe<InfrastructureMqttMessagingClientOptions>();
        serviceCollection.AddMessageConsumersHostedService();
        return serviceCollection;
    }
}