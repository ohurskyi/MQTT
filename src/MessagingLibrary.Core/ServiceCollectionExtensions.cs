using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageHandlerFactory<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: IMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<IMessageHandlerFactory<TMessagingClientOptions>, MessageHandlerFactory<TMessagingClientOptions>>();
        return serviceCollection;
    }
}