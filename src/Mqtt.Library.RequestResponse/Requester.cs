using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.MessageBus;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.RequestResponse;

public class Requester<TMessagingClientOptions> : IRequester<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessageBus<TMessagingClientOptions> _mqttMessageBus;
    private readonly IMqttTopicClient<TMessagingClientOptions> _mqttTopicClient;
    private readonly PendingResponsesTracker _pendingResponsesTracker;

    public Requester(
        IMqttMessageBus<TMessagingClientOptions> mqttMessageBus, 
        IMqttTopicClient<TMessagingClientOptions> mqttTopicClient, 
        PendingResponsesTracker pendingResponsesTracker)
    {
        _mqttMessageBus = mqttMessageBus;
        _pendingResponsesTracker = pendingResponsesTracker;
        _mqttTopicClient = mqttTopicClient;
    }

    public async Task<string> Request(string requestTopic, string responseTopic, IMessagePayload payload, TimeSpan timeout)
    {
        var correlationId = Guid.NewGuid();
        var replyTopic = $"{responseTopic}/{correlationId}";
        var subscription = await _mqttTopicClient.Subscribe<ResponseHandler>(replyTopic);
        var message = new Message { Topic = requestTopic, ReplyTopic = replyTopic, CorrelationId = correlationId, Payload = payload.MessagePayloadToJson() };
        var responseTask = await PublishAndWait(message);
        var response = await Task.WhenAny(responseTask, Task.Delay(timeout)) == responseTask
            ? responseTask.Result
            : null;
        await _mqttTopicClient.Unsubscribe(subscription);
        return response;
    }
    
    private async Task<Task<string>> PublishAndWait(IMessage message)
    {
        var tcs = _pendingResponsesTracker.AddCompletionSource(message.CorrelationId);
        await _mqttMessageBus.Publish(message);
        return tcs;
    }
}