using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mqtt.Library.Test.Client.Configuration;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;

namespace MessagingClient.Mqtt
{
    public class MqttMessagingClient<TMessagingClientOptions>
        : IMqttMessagingClient<TMessagingClientOptions>
        where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        private readonly IManagedMqttClient _mqttClient;
        private readonly ManagedMqttClientOptions _mqttClientOptions;

        public MqttMessagingClient(TMessagingClientOptions messagingClientOptions)
        {
            var clientOptions = new MqttClientOptionsBuilder()
                // for addition props this protocol should be used
                .WithProtocolVersion(MqttProtocolVersion.V500)
                .WithClientId($"Client_{typeof(TMessagingClientOptions).Name}_{Guid.NewGuid()}")
                .WithTcpServer(messagingClientOptions.MqttBrokerConnectionOptions.Host,
                    messagingClientOptions.MqttBrokerConnectionOptions.Port)
                .Build();

            _mqttClientOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(clientOptions)
                .Build();

            _mqttClient = new MqttFactory().CreateManagedMqttClient();
        }

        public void UseMqttMessageReceivedHandler(IMqttApplicationMessageReceivedHandler handler)
        {
            _mqttClient.UseApplicationMessageReceivedHandler(handler);
        }

        public async Task StartAsync()
        {
            await _mqttClient.StartAsync(_mqttClientOptions);
        }
        
        public async Task StopAsync()
        {
            await _mqttClient.StopAsync();
        }
        
        public Task SubscribeAsync(string topic)
        {
            return _mqttClient.SubscribeAsync(topic);
        }

        public Task SubscribeAsync(IEnumerable<string> topic)
        {
            return _mqttClient.SubscribeAsync(topic.Select(t => new MqttTopicFilterBuilder().WithTopic(t).Build()));
        }
        
        public Task UnsubscribeAsync(string topic)
        {
            return _mqttClient.UnsubscribeAsync(topic);
        }

        public Task UnsubscribeAsync(IEnumerable<string> topics)
        {
            return _mqttClient.UnsubscribeAsync(topics);
        }

        public async Task<MqttClientPublishResult> PublishAsync(MqttApplicationMessage mqttApplicationMessage)
        {
            // always will be mqttClientPublishResult.ReasonCode.Success
            var result = await _mqttClient.PublishAsync(mqttApplicationMessage);
            return result;
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing message client.");
            _mqttClient?.Dispose();
        }
    }
}