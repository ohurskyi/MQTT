using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;

namespace Mqtt.Library.Client.Infrastructure;

public class InfrastructureClientOptionsBuilder : ClientOptionsBuilder<InfrastructureClientOptions>
{
    public InfrastructureClientOptionsBuilder(InfrastructureClientOptions clientOptions) : base(clientOptions)
    {
    }
    
    public override ManagedMqttClientOptions BuildClientOptions()
    {
        var clientOptions = new MqttClientOptionsBuilder()
            .WithProtocolVersion(MqttProtocolVersion.V500)
            .WithClientId($"infrastructure_{Guid.NewGuid()}")
            .WithTcpServer(ClientOptions.MqttBrokerConnectionOptions.Host,
                ClientOptions.MqttBrokerConnectionOptions.Port)
            .Build();

        return new ManagedMqttClientOptionsBuilder()
            .WithAutoReconnectDelay(TimeSpan.FromSeconds(10))
            .WithClientOptions(clientOptions)
            .Build();
    }
}