using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
using DistributedConfiguration.Client.Listeners;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMessageHandler<UpdateLocalConfigurationMessageHandler>();
        serviceCollection.AddMessageHandler<NotifyUsersMessageHandler>();
        serviceCollection.AddMqttMessagingPipeline<InfrastructureMqttMessagingClientOptions>();
        serviceCollection.AddConsumerDefinitionListenerProvider<ConsumerDefinitionListenerProvider>();
        serviceCollection.AddConsumerListener();
        return serviceCollection;
    }
}