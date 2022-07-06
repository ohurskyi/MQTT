using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Messages;

namespace Mqtt.Library.RequestResponse;

public interface IRequestClient<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout) where TMessageResponse : class, IMessageResponse;
}