using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mqtt.Library.Client.Services
{
    public class MqttMessagingHostedService : IHostedService
    {
        private readonly IEnumerable<IMqttMessagingClient> _mqttMessagingClients;
        private readonly ILogger<MqttMessagingHostedService> _logger;

        public MqttMessagingHostedService(ILogger<MqttMessagingHostedService> logger, IEnumerable<IMqttMessagingClient> mqttMessagingClients)
        {
            _logger = logger;
            _mqttMessagingClients = mqttMessagingClients;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(_mqttMessagingClients.Select(x => x.StartAsync()).ToList());
            _logger.LogInformation($"Started {string.Join(", ", _mqttMessagingClients.Select(x => GetFriendlyName(x.GetType())))}");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(_mqttMessagingClients.Select(x => x.StopAsync()).ToList());
            _logger.LogInformation($"Stopped {string.Join(",", _mqttMessagingClients.Select(x => GetFriendlyName(x.GetType())))}");
        }

        private static string GetFriendlyName(Type type)
        {
            // we have only one generic arg
            return $"{type.Name}<{type.GenericTypeArguments[0].Name}>";
        }
    }
}