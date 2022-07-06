using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.RequestResponse.Mqtt.Requesters;

public interface IRequester<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<string> Request(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout);
}