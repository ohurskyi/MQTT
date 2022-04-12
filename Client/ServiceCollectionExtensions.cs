using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Mqtt.Library.Test.Client;
using Mqtt.Library.Test.Client.Configuration;
using Mqtt.Library.Test.Core;

namespace MessagingClient.Mqtt
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMqttMessagingClient<TMessagingClientOptions>(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
            where TMessagingClientOptions : class, IMqttMessagingClientOptions, new()
        {
            serviceCollection.ConfigureMessagingClientOptions<TMessagingClientOptions>(configuration);

            serviceCollection
                .AddSingleton<IMqttMessagingClient<TMessagingClientOptions>,
                    MqttMessagingClient<TMessagingClientOptions>>();

            // to get all clients in MqttMessagingHostedService
            serviceCollection.AddSingleton<IMqttMessagingClient>(pr =>
                pr.GetService<IMqttMessagingClient<TMessagingClientOptions>>());

            return serviceCollection;
        }

        public static IServiceCollection AddTopicClient<TMessagingClientOptions>(
            this IServiceCollection serviceCollection)
            where TMessagingClientOptions: IMqttMessagingClientOptions
        {
            serviceCollection.AddSingleton<ITopicClient<TMessagingClientOptions>, TopicClient<TMessagingClientOptions>>();
            return serviceCollection;
        }
        
        public static IServiceCollection AddMqttMessageBus<TMessagingClientOptions>(
            this IServiceCollection serviceCollection)
            where TMessagingClientOptions: IMqttMessagingClientOptions
        {
            serviceCollection.AddSingleton<IMqttMessageBus<TMessagingClientOptions>, MqttMqttMessageBus<TMessagingClientOptions>>();
            return serviceCollection;
        }
        
        public static IServiceCollection AddMqttMessagingStartupServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHostedService<MqttMessagingHostedService>();
            return serviceCollection;
        }
        
        public static IServiceCollection AddMqttApplicationMessageReceivedHandler(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<MqttReceivedMessageHandler>();
            return serviceCollection;
        }

        private static IServiceCollection ConfigureMessagingClientOptions<TMessagingClientOptions>(
            this IServiceCollection serviceCollection,
            IConfiguration configuration) where TMessagingClientOptions : class, IMqttMessagingClientOptions, new()
        {
            var sectionName = typeof(TMessagingClientOptions).Name;
            serviceCollection.Configure<TMessagingClientOptions>(
                configuration.GetSection(sectionName));
            serviceCollection.AddSingleton(sp => sp.GetRequiredService<IOptions<TMessagingClientOptions>>().Value);

            return serviceCollection;
        }
    }
}