using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Client.Services;

namespace Mqtt.Library.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMqttMessagingClient<TMessagingClientOptions>(this IServiceCollection serviceCollection, IConfiguration configuration)
            where TMessagingClientOptions : class, IMqttMessagingClientOptions, new()
        {
            serviceCollection.ConfigureMessagingClientOptions<TMessagingClientOptions>(configuration);

            serviceCollection.AddSingleton<IMqttMessagingClient<TMessagingClientOptions>, MqttMessagingClient<TMessagingClientOptions>>();

            // to get all clients in MqttMessagingHostedService
            serviceCollection.AddSingleton<IMqttMessagingClient>(pr => pr.GetService<IMqttMessagingClient<TMessagingClientOptions>>());

            return serviceCollection;
        }
        
        public static IServiceCollection AddMqttMessagingStartupServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHostedService<MqttMessagingHostedService>();
            return serviceCollection;
        }
        
        private static IServiceCollection ConfigureMessagingClientOptions<TMessagingClientOptions>(this IServiceCollection serviceCollection, IConfiguration configuration) 
            where TMessagingClientOptions : class, IMqttMessagingClientOptions, new()
        {
            var sectionName = typeof(TMessagingClientOptions).Name;
            serviceCollection.Configure<TMessagingClientOptions>(configuration.GetSection(sectionName));
            serviceCollection.AddSingleton(sp => sp.GetRequiredService<IOptions<TMessagingClientOptions>>().Value);

            return serviceCollection;
        }
    }
}