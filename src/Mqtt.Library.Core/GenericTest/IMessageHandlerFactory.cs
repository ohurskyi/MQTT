using System.Collections.Concurrent;
using MQTTnet.Server;

namespace Mqtt.Library.Core.GenericTest;

public interface IMessageHandlerFactoryGen
{
    int RegisterHandler<THandler>(string topic)
        where THandler : IMessageHandlerGen;

    IEnumerable<IMessageHandlerGen> GetHandlers(string topic, HandlerFactoryGen handlerFactory);
}

public class MessageHandlerFactoryGen : IMessageHandlerFactoryGen
{
    private readonly ConcurrentDictionary<string, ISet<Type>> _handlersMap = new();

    public int RegisterHandler<THandler>(string topic) where THandler : IMessageHandlerGen
    {
        if (!_handlersMap.TryGetValue(topic, out var handlers))
        {
            _handlersMap.TryAdd(topic, new HashSet<Type> { typeof(THandler) });
            return 1;
        }

        if (handlers.Contains(typeof(THandler)))
        {
            return handlers.Count;
        }

        handlers.Add(typeof(THandler));
        return handlers.Count;
    }

    public IEnumerable<IMessageHandlerGen> GetHandlers(string topic, HandlerFactoryGen handlerFactory)
    {
        var instances = _handlersMap
            .Where(k => MqttTopicFilterComparer.IsMatch(topic, k.Key))
            .SelectMany(k => k.Value)
            .Select(type => (IMessageHandlerGen)handlerFactory(type))
            .ToList();
        
        return instances;
    }
}