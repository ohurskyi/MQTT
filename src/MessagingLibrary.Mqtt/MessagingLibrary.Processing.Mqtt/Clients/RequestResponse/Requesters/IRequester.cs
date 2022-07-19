using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

namespace MessagingLibrary.RequestResponse.Mqtt.Requesters;

public interface IRequester<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<string> Request(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout);
}