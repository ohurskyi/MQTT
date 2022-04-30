using System.Collections.Concurrent;
using MQTTnet.Server;

namespace Mqtt.Library.Core.Factory;

public class MessageHandlerFactory : IMessageHandlerFactory
{
    private readonly ConcurrentDictionary<string, ISet<Type>> _handlersMap = new();

    public int RegisterHandler<THandler>(string topic) where THandler : IMessageHandler
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

    public IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory)
    {
        var instances = _handlersMap
            .Where(k => MqttTopicFilterComparer.IsMatch(topic, k.Key))
            .SelectMany(k => k.Value)
            .Select(type => (IMessageHandler)handlerFactory(type))
            .ToList();
        
        return instances;
    }
}