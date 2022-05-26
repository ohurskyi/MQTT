using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Factory;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Middleware;
using Mqtt.Library.Core.Results;

namespace Mqtt.Library.Core.Strategy;

public class MessageHandlingStrategy<T> : IMessageHandlingStrategy<T>, IDisposable where T : class
{
    private readonly IMessageHandlerFactory<T> _messageHandlerFactory;
    private readonly HandlerFactory _handlerFactory;
    private readonly ILogger<MessageHandlingStrategy<T>> _logger;

    public MessageHandlingStrategy(IMessageHandlerFactory<T> messageHandlerFactory, HandlerFactory handlerFactory, ILogger<MessageHandlingStrategy<T>> logger)
    {
        _messageHandlerFactory = messageHandlerFactory;
        _logger = logger;
        _handlerFactory = handlerFactory;
    }

    public async Task Handle(IMessage message)
    {
        var handlers = _messageHandlerFactory.GetHandlers(message.Topic, _handlerFactory);

        var funcs = handlers.Select(h => new Func<IMessage, Task<IExecutionResult>>(h.Handle));

        Task HandlerFunc() => HandleCore(funcs, message);

        var result = _handlerFactory
            .GetInstances<IMessageMiddleware>()
            .Reverse()
            .Aggregate((MessageHandlerDelegate)HandlerFunc, 
                (next, pipeline) => () => pipeline.Handle(message, next));

        await result();
    }

    protected virtual async Task<HandlerResult> HandleCore(IEnumerable<Func<IMessage, Task<IExecutionResult>>> handlers, IMessage message)
    {
        var executionResults = new List<IExecutionResult>();
        
        foreach (var handler in handlers)
        {
            var result = await handler(message);
            executionResults.Add(result);
        }

        var handlerResult = new HandlerResult();

        foreach (var executionResult in executionResults)
        {
            handlerResult.AddResult(executionResult);
        }

        return handlerResult;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(MessageHandlingStrategy<T>)} disposed.");
    }
}