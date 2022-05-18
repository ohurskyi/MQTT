﻿using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Factory;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Middleware;

namespace Mqtt.Library.Core.Strategy;

public class MessageHandlingStrategy : IMessageHandlingStrategy, IDisposable
{
    private readonly IMessageHandlerFactory _messageHandlerFactory;
    private readonly HandlerFactory _handlerFactory;
    private readonly ILogger<MessageHandlingStrategy> _logger;

    public MessageHandlingStrategy(IMessageHandlerFactory messageHandlerFactory, HandlerFactory handlerFactory, ILogger<MessageHandlingStrategy> logger)
    {
        _messageHandlerFactory = messageHandlerFactory;
        _logger = logger;
        _handlerFactory = handlerFactory;
    }

    public async Task Handle(IMessage message)
    {
        var handlers = _messageHandlerFactory.GetHandlers(message.Topic, _handlerFactory);

        var funcs = handlers.Select(h => new Func<IMessage, Task>(h.Handle));

        Task HandlerFunc() => HandleCore(funcs, message);

        var result = _handlerFactory
            .GetInstances<IMessageMiddleware>()
            .Reverse()
            .Aggregate((MessageHandlerDelegate)HandlerFunc, 
                (next, pipeline) => () => pipeline.Handle(message, next));

        await result();
    }

    protected virtual async Task HandleCore(IEnumerable<Func<IMessage, Task>> handlers, IMessage message)
    {
        foreach (var handler in handlers)
        {
            await handler(message);
        }
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(MessageHandlingStrategy)} disposed.");
    }
}