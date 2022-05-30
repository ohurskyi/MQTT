using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Middleware;
using Mqtt.Library.Core.Results;

namespace Mqtt.Library.Processing.Middlewares;

public class LoggingMiddleware : IMessageMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task<HandlerResult> Handle(IMessage message, MessageHandlerDelegate next)
    {
        _logger.LogInformation("Begin message handling on topic {value}", message.Topic);
        var result = await next();
        _logger.LogInformation("End message handling on topic {value}", message.Topic);
        return result;
    }
}