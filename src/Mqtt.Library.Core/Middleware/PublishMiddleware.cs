using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;

namespace Mqtt.Library.Core.Middleware;

public class PublishMiddleware : IMessageMiddleware
{
    private readonly ILogger<PublishMiddleware> _logger;

    public PublishMiddleware(ILogger<PublishMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task<HandlerResult> Handle(IMessage message, MessageHandlerDelegate next)
    {
        var result = await next();
        var integrationEvents = result.ExecutionResults.Where(r => r is IntegrationEventResult).ToList();
        return result;
    }
}