namespace Mqtt.Library.Test.Core;

public interface IMessageHandlerFactory
{
    IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory);
    void RegisterHandler<T>(string topic) where T : IMessageHandler;
}