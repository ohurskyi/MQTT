using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MessagingLibrary.Processing.Middlewares;
using Microsoft.Extensions.Logging;

namespace MessagingLibrary.Processing.Mqtt.Middlewares;

public class LoggingMiddleware : IMessageMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task<HandlerResult> Handle<TMessagingClientOptions>(IMessage message, MessageHandlerDelegate next) where TMessagingClientOptions : IMessagingClientOptions
    {
        _logger.LogInformation("Begin message handling on topic {value}", message.Topic);
        var result = await next();
        _logger.LogInformation("End message handling on topic {value}", message.Topic);
        return result;
    }
}