using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MessagingLibrary.Processing.Middlewares;

namespace MessagingLibrary.Processing.Strategy;

public class MessageHandlingStrategy<TMessagingClientOptions> : IMessageHandlingStrategy<TMessagingClientOptions>
    where TMessagingClientOptions: IMessagingClientOptions
{
    private readonly IMessageHandlerFactory<TMessagingClientOptions> _messageHandlerFactory;
    private readonly ServiceFactory _serviceFactory;

    public MessageHandlingStrategy(
        IMessageHandlerFactory<TMessagingClientOptions> messageHandlerFactory, 
        ServiceFactory serviceFactory)
    {
        _messageHandlerFactory = messageHandlerFactory;
        _serviceFactory = serviceFactory;
    }

    public async Task<HandlerResult> Handle(IMessage message)
    {
        var handlers = _messageHandlerFactory
            .GetHandlers(message.Topic, _serviceFactory)
            .Select(h => new Func<IMessage, Task<IExecutionResult>>(h.Handle));

        Task<HandlerResult> HandlerFunc() => HandleInner(handlers, message);

        var messageHandlerDelegate = _serviceFactory
            .GetInstances<IMessageMiddleware>()
            .Reverse()
            .Aggregate((MessageHandlerDelegate)HandlerFunc, 
                (next, pipeline) => () => pipeline.Handle<TMessagingClientOptions>(message, next));

        var handlerResult = await messageHandlerDelegate();

        return handlerResult;
    }

    private async Task<HandlerResult> HandleInner(IEnumerable<Func<IMessage, Task<IExecutionResult>>> handlers, IMessage message)
    {
        var executionResults = await HandleCore(handlers, message);

        var handlerResult = new HandlerResult();

        foreach (var executionResult in executionResults)
        {
            handlerResult.AddResult(executionResult);
        }

        return handlerResult;
    }

    protected virtual async Task<IEnumerable<IExecutionResult>> HandleCore(IEnumerable<Func<IMessage, Task<IExecutionResult>>> handlers, IMessage message)
    {
        var executionResults = new List<IExecutionResult>();

        foreach (var handler in handlers)
        {
            var result = await handler(message);
            executionResults.Add(result);
        }

        return executionResults;
    }
}