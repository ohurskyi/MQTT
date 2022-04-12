using System.Collections.Concurrent;
using Mqtt.Library.Test.Core;

namespace MqttClientTest.Messaging.Processing;

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
        if (!_handlersMap.TryGetValue(topic, out var types)) return Enumerable.Empty<IMessageHandler>();
        
        var instances = new List<IMessageHandler>(types.Count);
        instances.AddRange(types.Select(type => (IMessageHandler)handlerFactory(type)));
        return instances;
    }
}