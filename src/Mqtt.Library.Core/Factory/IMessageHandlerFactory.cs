namespace Mqtt.Library.Core.Factory;

public interface IMessageHandlerFactory
{
    int RegisterHandler<THandler>(string topic)
        where THandler : IMessageHandler;

    IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory);
}