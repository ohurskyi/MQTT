using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.RequestResponse;

public interface IRequester<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<string> Request(string requestTopic, string responseTopic, IMessagePayload payload, TimeSpan timeout);
}