using MQTTnet.Extensions.ManagedClient;

namespace MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

public abstract class ClientOptionsBuilder<TMessagingClientOptions> : IClientOptionsBuilder<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    protected readonly TMessagingClientOptions ClientOptions;

    protected ClientOptionsBuilder(TMessagingClientOptions clientOptions)
    {
        ClientOptions = clientOptions;
    }

    public abstract ManagedMqttClientOptions BuildClientOptions();
}