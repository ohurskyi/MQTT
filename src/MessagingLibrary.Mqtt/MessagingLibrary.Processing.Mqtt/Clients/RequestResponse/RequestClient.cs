using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Requesters;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

namespace MessagingLibrary.Processing.Mqtt.Clients.RequestResponse;

public class RequestClient<TMessagingClientOptions> : IRequestClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IRequester<TMessagingClientOptions> _requester;

    public RequestClient(IRequester<TMessagingClientOptions> requester)
    {
        _requester = requester;
    }

    public async Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout) where TMessageResponse : class, IMessageResponse
    {
        var response = await _requester.Request(requestTopic, responseTopic, contract, timeout);
        var messagePayloadFromJson = response?.MessagePayloadFromJson<TMessageResponse>();
        return messagePayloadFromJson;
    }
}