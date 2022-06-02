using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;
using Mqtt.Library.MessageBus;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.RequestResponse;

public class RequestClient<TMessagingClientOptions> : IRequestClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessageBus<TMessagingClientOptions> _mqttMessageBus;
    private readonly IMqttTopicClient<TMessagingClientOptions> _mqttTopicClient;

    public RequestClient(IMqttMessageBus<TMessagingClientOptions> mqttMessageBus, IMqttTopicClient<TMessagingClientOptions> mqttTopicClient)
    {
        _mqttMessageBus = mqttMessageBus;
        _mqttTopicClient = mqttTopicClient;
    }

    public async Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string topic, string replyTopic, IMessagePayload payload, TimeSpan timeout) where TMessageResponse : class, IMessageResponse
    {
        var subscription = await _mqttTopicClient.Subscribe<ResponseHandler>(replyTopic);
        await _mqttTopicClient.Unsubscribe(subscription);
        return null;
    }
}

public class ResponseHandler : IMessageHandler
{
    public Task<IExecutionResult> Handle(IMessage message)
    {
        throw new NotImplementedException();
    }
}