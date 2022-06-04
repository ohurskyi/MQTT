using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Results;

public class ReplyResult : ExecutionResult
{
    private ReplyResult(IMessagePayload messagePayload, Type messagingClientOptionsType, string replyTopic, Guid correlationId)
    {
        Payload = messagePayload;
        MessagingClientOptionsType = messagingClientOptionsType;
        ReplyTopic = replyTopic;
        CorrelationId = correlationId;
    }
        
    public static ReplyResult CreateIntegrationEventResult<TMessagingClientOptions>(IMessagePayload messagePayload, string replyTopic, Guid correlationId)
        where TMessagingClientOptions : class
    {
        return new ReplyResult(messagePayload, typeof(TMessagingClientOptions), replyTopic, correlationId);
    }

    public IMessagePayload Payload { get; }

    public Type MessagingClientOptionsType { get; }

    public string ReplyTopic { get; }

    public Guid CorrelationId { get; }
}