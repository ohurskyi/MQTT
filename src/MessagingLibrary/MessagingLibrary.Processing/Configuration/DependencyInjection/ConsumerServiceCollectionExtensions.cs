using MessagingLibrary.Processing.Listeners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Processing.Configuration.DependencyInjection;

public static class ConsumerServiceCollectionExtensions
{
    public static IServiceCollection AddConsumerDefinitionListenerProvider<TConsumerDefinitionListenerProvider>(this IServiceCollection serviceCollection)
        where TConsumerDefinitionListenerProvider: class, IConsumerDefinitionListenerProvider
    {
        serviceCollection.TryAddSingleton<IConsumerDefinitionListenerProvider, TConsumerDefinitionListenerProvider>();
        return serviceCollection;
    }

    public static IServiceCollection AddConsumerListener(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<MessageConsumersHostedService>();
        return serviceCollection;
    }
}