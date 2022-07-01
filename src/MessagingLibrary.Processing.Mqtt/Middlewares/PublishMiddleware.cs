using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MessagingLibrary.Processing.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MessagingLibrary.Processing.Mqtt.Middlewares;

public class PublishMiddleware : IMessageMiddleware
{
    private readonly ILogger<PublishMiddleware> _logger;
    private readonly IServiceProvider _serviceProvider;

    public PublishMiddleware(ILogger<PublishMiddleware> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<HandlerResult> Handle<TMessagingClientOptions>(IMessage message, MessageHandlerDelegate next)
        where TMessagingClientOptions : IMessagingClientOptions
    {
        var result = await next();
        var integrationEvents = result.ExecutionResults.OfType<IntegrationEventResult>().ToList();
        var publishTasks = new List<Task>(integrationEvents.Count);
        foreach (var integrationEvent in integrationEvents)
        {
            _logger.LogInformation("Publishing integration event into topic {topicValue} of payload {type}", integrationEvent.Topic, integrationEvent.Payload.GetType().Name);
            var messageBus = _serviceProvider.GetRequiredService<IMessageBus<TMessagingClientOptions>>();
            publishTasks.Add(messageBus.Publish(integrationEvent.Payload, integrationEvent.Topic));
        }

        await Task.WhenAll(publishTasks);
        return result;
    }
}