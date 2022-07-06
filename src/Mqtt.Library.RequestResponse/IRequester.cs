using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Messages;

namespace Mqtt.Library.RequestResponse;

public interface IRequester<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<string> Request(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout);
}