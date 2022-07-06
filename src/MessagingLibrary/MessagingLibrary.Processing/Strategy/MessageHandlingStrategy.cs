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
    private readonly HandlerFactory _handlerFactory;

    public MessageHandlingStrategy(
        IMessageHandlerFactory<TMessagingClientOptions> messageHandlerFactory, 
        HandlerFactory handlerFactory)
    {
        _messageHandlerFactory = messageHandlerFactory;
        _handlerFactory = handlerFactory;
    }

    public async Task Handle(IMessage message)
    {
        var handlers = _messageHandlerFactory.GetHandlers(message.Topic, _handlerFactory);

        var funcs = handlers.Select(h => new Func<IMessage, Task<IExecutionResult>>(h.Handle));

        Task<HandlerResult> HandlerFunc() => HandleCore(funcs, message);

        var result = _handlerFactory
            .GetInstances<IMessageMiddleware>()
            .Reverse()
            .Aggregate((MessageHandlerDelegate)HandlerFunc, 
                (next, pipeline) => () => pipeline.Handle<TMessagingClientOptions>(message, next));

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
}