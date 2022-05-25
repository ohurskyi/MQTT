using System.Collections.Concurrent;

namespace Mqtt.Library.Core.Factory;

public class MessageHandlerFactory<T> : IMessageHandlerFactory<T> where T : class
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<Type, byte>> _handlersMap = new();
    
    private readonly ITopicFilterComparer _topicFilterComparer;

    public MessageHandlerFactory(ITopicFilterComparer topicFilterComparer)
    {
        _topicFilterComparer = topicFilterComparer;
    }


    public int RegisterHandler<THandler>(string topic) where THandler : IMessageHandler
    {
        return AddInner<THandler>(topic);
    }

    public int RemoveHandler<THandler>(string topic) where THandler : IMessageHandler
    {
        return RemoveInner(typeof(THandler), topic);
    }

    public int RemoveHandler(Type handlerType, string topic)
    {
        return RemoveInner(handlerType, topic);
    }

    public IEnumerable<IMessageHandler> GetHandlers(string topic, HandlerFactory handlerFactory)
    {
        var instances = _handlersMap
            .Where(k => _topicFilterComparer.IsMatch(topic, k.Key))
            .SelectMany(k => k.Value.Keys)
            .Select(handlerFactory.GetInstance<IMessageHandler>)
            .ToList();
        
        return instances;
    }

    private int AddInner<THandler>(string topic) where THandler : IMessageHandler
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
    
    private int RemoveInner(Type handlerType, string topic)
    {
        if (!_handlersMap.TryGetValue(topic, out var handlers))
        {
            return -1;
        }

        handlers.TryRemove(handlerType, out _);

        return handlers.Count;
    }
}