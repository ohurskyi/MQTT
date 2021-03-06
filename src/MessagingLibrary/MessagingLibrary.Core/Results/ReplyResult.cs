using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Core.Results;

public class ReplyResult : SuccessfulResult
{
    public ReplyResult(IMessageResponse messagePayload, string replyTopic, Guid correlationId)
    {
        Payload = messagePayload;
        ReplyTopic = replyTopic;
        CorrelationId = correlationId;
    }

    public IMessageResponse Payload { get; }
    
    public string ReplyTopic { get; }

    public Guid CorrelationId { get; }
}