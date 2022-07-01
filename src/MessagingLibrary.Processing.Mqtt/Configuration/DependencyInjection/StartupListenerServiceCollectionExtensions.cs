using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class StartupListenerServiceCollectionExtensions
{
    public static IServiceCollection AddMqttStartupListener<TStartupListener>(this IServiceCollection serviceCollection)
        where TStartupListener: class, IHostedService
    {
        serviceCollection.AddHostedService<TStartupListener>();
        return serviceCollection;
    }
}