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
        var correlationId = Guid.NewGuid();
        var replyTopic = $"{responseTopic}/{correlationId}";
        var subscription = await _mqttTopicClient.Subscribe<ResponseHandler>(replyTopic);
        var message = new Message { Topic = requestTopic, ReplyTopic = replyTopic, CorrelationId = correlationId, Payload = payload.MessagePayloadToJson() };
        var responseTask = await PublishAndWait(message);
        var response = await Task.WhenAny(responseTask, Task.Delay(timeout)) == responseTask
            ? responseTask.Result
            : null;
        var messagePayloadFromJson = response?.MessagePayloadFromJson<TMessageResponse>();
        await _mqttTopicClient.Unsubscribe(subscription);
        return messagePayloadFromJson;
    }

    private async Task<Task<string>> PublishAndWait(IMessage message)
    {
        var tcs = _pendingResponsesTracker.AddCompletionSource(message.CorrelationId);
        await _mqttMessageBus.Publish(message);
        return tcs;
    }
}