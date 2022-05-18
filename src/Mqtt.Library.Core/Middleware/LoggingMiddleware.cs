using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Middleware;

public class LoggingMiddleware : IMessageMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Handle(IMessage message, MessageHandlerDelegate next)
    {
        _logger.LogInformation("Begin message handling on topic {value}", message.Topic);
        await next();
        _logger.LogInformation("End message handling on topic {value}", message.Topic);
    }
}