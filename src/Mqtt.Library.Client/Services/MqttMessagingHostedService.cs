﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Client.Services
{
    public class MqttMessagingHostedService<TMessagingClientOptions> : IHostedService
        where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;
        private readonly ILogger<MqttMessagingHostedService<TMessagingClientOptions>> _logger;

        public MqttMessagingHostedService(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient, ILogger<MqttMessagingHostedService<TMessagingClientOptions>> logger)
        {
            _mqttMessagingClient = mqttMessagingClient;
            _logger = logger;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _mqttMessagingClient.StartAsync();
            _logger.LogInformation("Started {client} ", GetFriendlyName(_mqttMessagingClient.GetType()));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttMessagingClient.StopAsync();
            _logger.LogInformation("Stopped {client} ", GetFriendlyName(_mqttMessagingClient.GetType()));
        }

        private static string GetFriendlyName(Type type)
        {
            return $"{type.Name}<{type.GenericTypeArguments[0].Name}>";
        }
    }
}