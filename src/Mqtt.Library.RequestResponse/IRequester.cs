using MessagingLibrary.Core.Messages;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.RequestResponse;

public interface IRequester<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<string> Request(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout);
}