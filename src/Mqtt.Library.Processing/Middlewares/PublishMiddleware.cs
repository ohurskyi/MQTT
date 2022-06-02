using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Middleware;
using Mqtt.Library.Core.Results;
using Mqtt.Library.MessageBus;

namespace Mqtt.Library.Processing.Middlewares;

public class PublishMiddleware : IMessageMiddleware
{
    private readonly ILogger<PublishMiddleware> _logger;
    private readonly IServiceProvider _serviceProvider;

    public PublishMiddleware(ILogger<PublishMiddleware> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<HandlerResult> Handle(IMessage message, MessageHandlerDelegate next)
    {
        var result = await next();
        var integrationEvents = result.ExecutionResults.OfType<IntegrationEventResult>().ToList();
        var publishTasks = new List<Task>(integrationEvents.Count);
        foreach (var integrationEvent in integrationEvents)
        {
            _logger.LogInformation("Publishing integration event {type}", integrationEvent.Payload.GetType().Name);
            var eventBusType = typeof(IMqttMessageBus<>).MakeGenericType(integrationEvent.MessagingClientOptionsType);
            var eventBus = (IMqttMessageBus)_serviceProvider.GetRequiredService(eventBusType);
            publishTasks.Add(eventBus.Publish(integrationEvent.Payload, integrationEvent.Topic));
        }
        await Task.WhenAll(publishTasks);
        return result;
    }
}