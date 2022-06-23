namespace MessagingLibrary.Core.Handlers;

public interface IMessageHandlerDefinition
{
    Type HandlerType { get; }
    
    string Topic { get; }
}

public interface IMessageHandlerDefinition<TMessageHandler> : IMessageHandlerDefinition
    where TMessageHandler : class, IMessageHandler
{
}

public class MessageHandlerDefinition<TMessageHandler> : IMessageHandlerDefinition<TMessageHandler> where TMessageHandler : class, IMessageHandler
{
    public MessageHandlerDefinition(string topic)
    {
        Topic = topic;
    }

    public Type HandlerType => typeof(TMessageHandler);
    public string Topic { get; }
}