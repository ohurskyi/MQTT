namespace Mqtt.Library.Core.Factory;

public interface IMessageHandlerFactory
{
    int RegisterHandler<THandler>(string topic) where THandler : IMessageHandler;
    int RemoveHandler<THandler>(string topic) where THandler : IMessageHandler;
    int RemoveHandler(Type handlerType, string topic);
    IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory);
}