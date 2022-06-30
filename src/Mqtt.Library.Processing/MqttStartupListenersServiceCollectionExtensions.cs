using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mqtt.Library.Processing;

public static class MqttStartupListenersServiceCollectionExtensions
{
    public static IServiceCollection AddMqttStartupListener<TStartupListener>(this IServiceCollection serviceCollection)
        where TStartupListener: class, IHostedService
    {
        serviceCollection.AddHostedService<TStartupListener>();
        return serviceCollection;
    }
}