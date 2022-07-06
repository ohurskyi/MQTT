using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.RequestResponse.Mqtt.Requesters;

namespace MessagingLibrary.RequestResponse.Mqtt;

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