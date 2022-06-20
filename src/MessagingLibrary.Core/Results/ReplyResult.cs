using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Core.Results;

public class ReplyResult : ExecutionResult
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