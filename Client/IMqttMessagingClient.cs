using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mqtt.Library.Test.Client.Configuration;
using MQTTnet;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;

namespace MessagingClient.Mqtt
{
    public interface IMqttMessagingClient<in TMessagingClientOptions> : IMqttMessagingClient
        where TMessagingClientOptions : IMqttMessagingClientOptions
    {
    }

    public interface IMqttMessagingClient : IDisposable
    {
        Task StartAsync();

        Task StopAsync();

        Task SubscribeAsync(string topic);

        Task SubscribeAsync(IEnumerable<string> topic);
        
        Task UnsubscribeAsync(string topic);
        
        Task UnsubscribeAsync(IEnumerable<string> topics);
        
        Task<MqttClientPublishResult> PublishAsync(MqttApplicationMessage mqttApplicationMessage);

        void UseMqttMessageReceivedHandler(IMqttApplicationMessageReceivedHandler handler);
    }
}