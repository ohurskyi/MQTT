namespace MessagingLibrary.Core.Messages;

public interface IMessagingContext<T> where T: IMessageContract
{
    string Topic { get; set; }
    string ReplyTopic { get; set; }
    T Payload { get; set; }
    Guid CorrelationId { get; set; }
}