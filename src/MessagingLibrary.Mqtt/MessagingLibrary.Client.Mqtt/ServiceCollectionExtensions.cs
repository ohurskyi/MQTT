using MessagingLibrary.Client.Mqtt.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Mqtt.Library.Client.Services;

namespace MessagingLibrary.Client.Mqtt
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMqttMessagingClient<TMessagingClientOptions>(this IServiceCollection serviceCollection, IConfiguration configuration)
            where TMessagingClientOptions : class, IMqttMessagingClientOptions, new()
        {
            serviceCollection.ConfigureMessagingClientOptions<TMessagingClientOptions>(configuration);

            serviceCollection.TryAddSingleton<IMqttMessagingClient<TMessagingClientOptions>, MqttMessagingClient<TMessagingClientOptions>>();
            
            serviceCollection.AddMqttMessagingStartupServices<TMessagingClientOptions>();

            return serviceCollection;
        }

        private static IServiceCollection AddMqttMessagingStartupServices<TMessagingClientOptions>(this IServiceCollection serviceCollection)
            where TMessagingClientOptions : class, IMqttMessagingClientOptions, new()
        {
            serviceCollection.AddHostedService<MqttMessagingHostedService<TMessagingClientOptions>>();
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