using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Middleware;
using Mqtt.Library.Core.Results;
using Mqtt.Library.MessageBus;

namespace Mqtt.Library.Processing.Middlewares;

public class ReplyMiddleware : IMessageMiddleware
{
    private readonly ILogger<ReplyMiddleware> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ReplyMiddleware(ILogger<ReplyMiddleware> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<HandlerResult> Handle(IMessage message, MessageHandlerDelegate next)
    {
        var result = await next();
        var replyResults = result.ExecutionResults.OfType<ReplyResult>().ToList();
        var replyTasks = new List<Task>(replyResults.Count);
        foreach (var replyResult in replyResults)
        {
            _logger.LogInformation("Sending reply to {topic}", replyResult.ReplyTopic);
            var eventBusType = typeof(IMqttMessageBus<>).MakeGenericType(replyResult.MessagingClientOptionsType);
            var eventBus = (IMqttMessageBus)_serviceProvider.GetRequiredService(eventBusType);
            var replyMessage = new Message { Topic = replyResult.ReplyTopic, CorrelationId = replyResult.CorrelationId, Payload = replyResult.Payload.MessagePayloadToJson() };
            replyTasks.Add(eventBus.Publish(replyMessage));
        }
        await Task.WhenAll(replyTasks);
        return result;
    }
}