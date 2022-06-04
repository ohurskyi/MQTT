using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.MessageBus;
using Mqtt.Library.TopicClient;
using Newtonsoft.Json;

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

    public async Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string topic, string replyTopic, IMessagePayload payload, TimeSpan timeout) where TMessageResponse : class, IMessageResponse
    {
        var subscription = await _mqttTopicClient.Subscribe<ResponseHandler>(replyTopic);
        var correlationId = Guid.NewGuid();
        var message = new Message { Topic = topic, ReplyTopic = $"{replyTopic}/{correlationId}", CorrelationId = correlationId, Payload = payload.MessagePayloadToJson() };
        var responseTask = await PublishAndWait(message);
        var response = await Task.WhenAny(responseTask, Task.Delay(timeout)) == responseTask
            ? responseTask.Result
            : null;
        var messagePayloadFromJson = response.MessagePayloadFromJson<TMessageResponse>();
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