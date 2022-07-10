using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Handlers;

namespace MessagingLibrary.Core.Factory;

public interface IMessageHandlerFactory
{
    int RegisterHandler(Type handlerType, string topic);
    int RegisterHandler<THandler>(string topic) where THandler : class, IMessageHandler;
    int RemoveHandler<THandler>(string topic) where THandler : class, IMessageHandler;
    int RemoveHandler(Type handlerType, string topic);
    IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory);
}

public interface IMessageHandlerFactory<TMessagingClientOptions> : IMessageHandlerFactory
    where TMessagingClientOptions: IMessagingClientOptions
{
    
}