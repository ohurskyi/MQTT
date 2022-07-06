using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.RequestResponse.Mqtt;

public interface IRequestClient<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout) where TMessageResponse : class, IMessageResponse;
}