using DistributedConfiguration.Client.Consumers;
using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
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
        serviceCollection.AddMqttPipe<InfrastructureMqttMessagingClientOptions>();
        serviceCollection.AddConsumerDefinitionListenerProvider<ConsumerDefinitionListenerProvider>();
        serviceCollection.AddMessageConsumersHostedService();
        return serviceCollection;
    }
}