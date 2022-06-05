using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.MessageBus;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.RequestResponse;

public class RequestClient<TMessagingClientOptions> : IRequestClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessageBus<TMessagingClientOptions> _mqttMessageBus;
    private readonly IMqttTopicClient<TMessagingClientOptions> _mqttTopicClient;
    private readonly PendingResponsesTracker _pendingResponsesTracker;

    public RequestClient(
        IMqttMessageBus<TMessagingClientOptions> mqttMessageBus, 
        IMqttTopicClient<TMessagingClientOptions> mqttTopicClient, 
        PendingResponsesTracker pendingResponsesTracker)
    {
        _mqttMessageBus = mqttMessageBus;
        _mqttTopicClient = mqttTopicClient;
        _pendingResponsesTracker = pendingResponsesTracker;
    }

    public async Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessagePayload payload, TimeSpan timeout) where TMessageResponse : class, IMessageResponse
    {
        var requester = new Requester<TMessagingClientOptions>(responseTopic, _mqttMessageBus, _mqttTopicClient, _pendingResponsesTracker);
        var response = await requester.Request(requestTopic, payload, timeout);
        var messagePayloadFromJson = response?.MessagePayloadFromJson<TMessageResponse>();
        return messagePayloadFromJson;
    }
}