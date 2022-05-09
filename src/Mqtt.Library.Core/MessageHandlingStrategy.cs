﻿using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Factory;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core;

public class MessageHandlingStrategy : IMessageHandlingStrategy, IDisposable
{
    private readonly IMessageHandlerFactory _messageHandlerFactory;
    private readonly ILogger<MessageHandlingStrategy> _logger;

    public MessageHandlingStrategy(IMessageHandlerFactory messageHandlerFactory, ILogger<MessageHandlingStrategy> logger)
    {
        _messageHandlerFactory = messageHandlerFactory;
        _logger = logger;
    }

    public async Task Handle(IMessage message, HandlerFactory handlerFactory)
    {
        var handlers = _messageHandlerFactory.GetHandlers(message.Topic, handlerFactory);
        
        var funcs = handlers.Select(h => new Func<IMessage, Task>(h.Handle));
        
        await HandleStrategy(funcs, message);
    }

    protected virtual async Task HandleStrategy(IEnumerable<Func<IMessage, Task>> handlers, IMessage message)
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