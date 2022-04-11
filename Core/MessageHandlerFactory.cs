using System.Collections.Concurrent;
using Mqtt.Library.Test.Core;

namespace MqttClientTest.Messaging.Processing;

public class MessageHandlerFactory : IMessageHandlerFactory
{
    private static readonly ConcurrentDictionary<string, ISet<Type>> _handlersMap = new();

    private readonly HandlerFactory _handlerFactory;
    private readonly IServiceProvider _serviceProvider;

    public MessageHandlerFactory(HandlerFactory handlerFactory, IServiceProvider serviceProvider)
    {
        _handlerFactory = handlerFactory;
        _serviceProvider = serviceProvider;
    }
        
    public IEnumerable<IMessageHandler> GetHandlers(string topic)
    {
        return GetHandlersInternal(topic);
    }

    public void RegisterHandler<T>(string topic) where T : IMessageHandler
    {
        RegisterHandlerInternal<T>(topic);
    }

    private static void RegisterHandlerInternal<T>(string topic) where T : IMessageHandler
    {
        if (!_handlersMap.TryGetValue(topic, out var handlers))
        {
            _handlersMap.TryAdd(topic, new HashSet<Type> { typeof(T) });
            return;
        }

        if (handlers.Contains(typeof(T)))
        {
            return;
        }

        handlers.Add(typeof(T));
    }

    private IEnumerable<IMessageHandler> GetHandlersInternal(string topic)
    {
        if (!_handlersMap.TryGetValue(topic, out var types)) return Enumerable.Empty<IMessageHandler>();
            
        var instances = new List<IMessageHandler>(types.Count);
        instances.AddRange(types.Select(type => (IMessageHandler)_handlerFactory(type)));
        return instances;

    }
}