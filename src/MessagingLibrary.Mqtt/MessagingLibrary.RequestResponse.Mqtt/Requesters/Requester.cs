using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.RequestResponse.Mqtt.Completion;
using MessagingLibrary.RequestResponse.Mqtt.Handlers;
using MessagingLibrary.TopicClient.Mqtt;

namespace MessagingLibrary.RequestResponse.Mqtt.Requesters;

public class Requester<TMessagingClientOptions> : IRequester<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMessageBus<TMessagingClientOptions> _mqttMessageBus;
    private readonly ITopicClient<TMessagingClientOptions> _mqttTopicClient;
    private readonly PendingResponseTracker _pendingResponseTracker;

    public Requester(
        IMessageBus<TMessagingClientOptions> mqttMessageBus, 
        ITopicClient<TMessagingClientOptions> mqttTopicClient,
        PendingResponseTracker pendingResponseTracker)
    {
        _mqttMessageBus = mqttMessageBus;
        _pendingResponseTracker = pendingResponseTracker;
        _mqttTopicClient = mqttTopicClient;
    }

    public async Task<string> Request(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout)
    {
        var correlationId = Guid.NewGuid();
        var replyTopic = $"{responseTopic}/{correlationId}";
        var subscription = await _mqttTopicClient.Subscribe<ResponseHandler>(replyTopic);
        
        try
        {
            var message = new Message { Topic = requestTopic, ReplyTopic = replyTopic, CorrelationId = correlationId, Payload = contract.MessagePayloadToJson() };
            var responseTask = await PublishAndWaitForCompletion(message);
            var delayTask = Task.Delay(timeout);
            
            if (await Task.WhenAny(responseTask, delayTask) == delayTask)
            {
                throw new OperationCanceledException();
            }

            return responseTask.Result;
        }
        finally
        {
            await _mqttTopicClient.Unsubscribe(subscription);
            _pendingResponseTracker.RemoveCompletion(correlationId);
        }
    }
    
    private async Task<Task<string>> PublishAndWaitForCompletion(IMessage message)
    {
        var tcs = _pendingResponseTracker.AddCompletion(message.CorrelationId);
        await _mqttMessageBus.Publish(message);
        return tcs;
    }
}