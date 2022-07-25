using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;

namespace MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

public class DefaultClientOptionsBuilder<TMessagingClientOptions> : ClientOptionsBuilder<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    public DefaultClientOptionsBuilder(TMessagingClientOptions clientOptions) : base(clientOptions)
    {
    }

    public override ManagedMqttClientOptions BuildClientOptions()
    {
        var clientOptions = new MqttClientOptionsBuilder()
            .WithProtocolVersion(MqttProtocolVersion.V500)
            .WithClientId($"Client_{typeof(TMessagingClientOptions).Name}_{Guid.NewGuid()}")
            .WithTcpServer(ClientOptions.MqttBrokerConnectionOptions.Host,
                ClientOptions.MqttBrokerConnectionOptions.Port)
            .Build();

        return new ManagedMqttClientOptionsBuilder()
            .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
            .WithClientOptions(clientOptions)
            .Build();
    }
}