namespace Mqtt.Library.Test.Core;

public interface IMessageHandlerFactory
{
    IEnumerable<IMessageHandler> GetHandlers(string topic);
    void RegisterHandler<T>(string topic) where T : IMessageHandler;
    IEnumerable<IMessageHandler> GetHandlers(string topic, IServiceProvider scopedServiceProvider);
}