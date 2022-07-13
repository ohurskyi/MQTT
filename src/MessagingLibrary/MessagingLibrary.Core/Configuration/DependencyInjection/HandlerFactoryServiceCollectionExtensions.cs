using MessagingLibrary.Core.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Core.Configuration.DependencyInjection;

public static class HandlerFactoryServiceCollectionExtensions
{
    public static IServiceCollection AddMessageHandlerFactory<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: IMessagingClientOptions
    {
        serviceCollection.AddOptionalServiceResolvingFactory();
        serviceCollection.TryAddSingleton<IMessageHandlerFactory<TMessagingClientOptions>, MessageHandlerFactory<TMessagingClientOptions>>();
        return serviceCollection;
    }

    /// <summary>
    /// optional because in DI can be registered MessageHandler1, MessageHandler2
    /// but for a moment only MessageHandler1 subscribed to topic
    /// in this case we won't have NullReferenceException while creating handlers on the fly
    /// will get null and can skip it.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionalServiceResolvingFactory(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddTransient<ServiceFactory>(p => p.GetService);
        return serviceCollection;
    }
}