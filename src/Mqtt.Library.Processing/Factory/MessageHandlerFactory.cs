using System.Collections.Concurrent;
using Mqtt.Library.Core;
using MQTTnet.Server;

namespace Mqtt.Library.Processing.Factory;

public class MessageHandlerFactory : IMessageHandlerFactory
{
    private static readonly ConcurrentDictionary<string, ISet<Type>> _handlersMap = new();
    
    public IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory)
    {
        return GetHandlersInternalWithHandlerFactory(topic, handlerFactory);
    }

    public int RegisterHandler<T>(string topic) where T : IMessageHandler
    {
        return RegisterHandlerInternal<T>(topic);
    }

    private static int RegisterHandlerInternal<T>(string topic) where T : IMessageHandler
    {
        if (!_handlersMap.TryGetValue(topic, out var handlers))
        {
            _handlersMap.TryAdd(topic, new HashSet<Type> { typeof(T) });
            return 1;
        }

        if (handlers.Contains(typeof(T)))
        {
            return handlers.Count;
        }

        handlers.Add(typeof(T));
        return handlers.Count;
    }

    private static IEnumerable<IMessageHandler> GetHandlersInternalWithHandlerFactory(string topic, HandlerFactory handlerFactory)
    {
        var instances = _handlersMap
            .Where(k => MqttTopicFilterComparer.IsMatch(topic, k.Key))
            .SelectMany(k => k.Value)
            .Select(type => (IMessageHandler)handlerFactory(type))
            .ToList();
        
        return instances;
    }
}