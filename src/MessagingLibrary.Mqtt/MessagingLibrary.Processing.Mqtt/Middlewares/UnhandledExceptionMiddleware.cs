using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MessagingLibrary.Processing.Middlewares;
using Microsoft.Extensions.Logging;

namespace MessagingLibrary.Processing.Mqtt.Middlewares;

public class UnhandledExceptionMiddleware : IMessageMiddleware
{
    private readonly ILogger<UnhandledExceptionMiddleware> _logger;

    public UnhandledExceptionMiddleware(ILogger<UnhandledExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task<HandlerResult> Handle<TMessagingClientOptions>(IMessage message, MessageHandlerDelegate next) where TMessagingClientOptions : IMessagingClientOptions
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled Exception while processing message on topic {topicValue}", message.Topic);
            throw;
        }
    }
}