using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Core.Clients;

public interface IRequestClient<TMessagingClientOptions> where TMessagingClientOptions : IMessagingClientOptions
{
    Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout) where TMessageResponse : class, IMessageResponse;
}