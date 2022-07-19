using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

namespace MessagingLibrary.Processing.Mqtt.Clients.RequestResponse;

public interface IRequestClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout) where TMessageResponse : class, IMessageResponse;
}