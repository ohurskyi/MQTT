namespace Mqtt.Library.Processing.Factory;

public interface IMessageHandlerFactory
{
    IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory);
    int RegisterHandler<T>(string topic) where T : IMessageHandler;
}