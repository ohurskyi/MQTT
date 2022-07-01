using MessagingLibrary.Processing.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mqtt.Library.Processing.Configuration.DependencyInjection;

public static class MiddlewareServiceCollectionsExtensions
{
    public static IServiceCollection AddMiddleware<T>(this IServiceCollection serviceCollection) where T : class, IMessageMiddleware
    {
        serviceCollection.TryAddEnumerable(new [] {ServiceDescriptor.Transient<IMessageMiddleware, T>() });
        return serviceCollection;
    }
}