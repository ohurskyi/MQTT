using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Completion;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Requesters;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

namespace MessagingLibrary.Processing.Mqtt.Clients.RequestResponse;

public class RequestClient<TMessagingClientOptions> : IRequestClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMessageBus<TMessagingClientOptions> _mqttMessageBus;
    private readonly ITopicClient<TMessagingClientOptions> _mqttTopicClient;
    private readonly PendingResponseTracker _pendingResponseTracker;

    public RequestClient(IMessageBus<TMessagingClientOptions> mqttMessageBus, ITopicClient<TMessagingClientOptions> mqttTopicClient, PendingResponseTracker pendingResponseTracker)
    {
        _mqttMessageBus = mqttMessageBus;
        _mqttTopicClient = mqttTopicClient;
        _pendingResponseTracker = pendingResponseTracker;
    }

    public async Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout) where TMessageResponse : class, IMessageResponse
    {
        var requester = new Requester<TMessagingClientOptions>(_mqttMessageBus, _mqttTopicClient, _pendingResponseTracker);
        var response = await requester.Request(requestTopic, responseTopic, contract, timeout);
        var messagePayloadFromJson = response?.MessagePayloadFromJson<TMessageResponse>();
        return messagePayloadFromJson;
    }
}