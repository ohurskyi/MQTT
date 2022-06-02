using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.RequestResponse;

public interface IRequestClient<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string topic, string replyTopic, IMessagePayload payload, TimeSpan timeout) where TMessageResponse : class, IMessageResponse;
}