using System.Collections.Concurrent;
using MQTTnet.Server;

namespace Mqtt.Library.Core.Factory;

public class MessageHandlerFactory : IMessageHandlerFactory
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<Type, byte>> _handlersMap = new();

    public int RegisterHandler<THandler>(string topic) where THandler : IMessageHandler
    {
        if (!_handlersMap.TryGetValue(topic, out var handlers))
        {
            _handlersMap.TryAdd(topic,
                new ConcurrentDictionary<Type, byte>(new[]
                    { new KeyValuePair<Type, byte>(typeof(THandler), default) })
            );
            return 1;
        }

        if (handlers.ContainsKey(typeof(THandler)))
        {
            return handlers.Count;
        }

        handlers.TryAdd(typeof(THandler), default);
        
        return handlers.Count;
    }

    public int RemoveHandler<THandler>(string topic) where THandler : IMessageHandler
    {
        if (!_handlersMap.TryGetValue(topic, out var handlers))
        {
            return -1;
        }

        handlers.TryRemove(typeof(THandler), out _);
        
        return handlers.Count;
    }

    public IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory)
    {
        var instances = _handlersMap
            .Where(k => MqttTopicFilterComparer.IsMatch(topic, k.Key))
            .SelectMany(k => k.Value.Keys)
            .Select(handlerFactory.GetHandler<IMessageHandler>)
            .ToList();
        
        return instances;
    }
}