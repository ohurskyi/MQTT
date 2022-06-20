namespace MessagingLibrary.Core.Messages;

public class MessagingContext<T> : IMessagingContext<T> where T : IMessagePayload
{
    public string Topic { get; set; }
    public string ReplyTopic { get; set; }
    public T Payload { get; set; }
    public Guid CorrelationId { get; set; }
}