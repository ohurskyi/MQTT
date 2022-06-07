using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.RequestResponse;

public class RequestClient<TMessagingClientOptions> : IRequestClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IRequester<TMessagingClientOptions> _requester;

    public RequestClient(IRequester<TMessagingClientOptions> requester)
    {
        _requester = requester;
    }

    public async Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessagePayload payload, TimeSpan timeout) where TMessageResponse : class, IMessageResponse
    {
        var response = await _requester.Request(requestTopic, responseTopic, payload, timeout);
        var messagePayloadFromJson = response?.MessagePayloadFromJson<TMessageResponse>();
        return messagePayloadFromJson;
    }
}