using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MessagingLibrary.Processing.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

    public async Task<HandlerResult> Handle<TMessagingClientOptions>(IMessage message, MessageHandlerDelegate next) where TMessagingClientOptions : IMessagingClientOptions
    {
        var result = await next();
        var replyResults = result.ExecutionResults.OfType<ReplyResult>().ToList();
        var replyTasks = new List<Task>(replyResults.Count);
        foreach (var replyResult in replyResults)
        {
            _logger.LogInformation("Sending reply to topic {topicValue} of payload {type}", replyResult.ReplyTopic,  replyResult.Payload.GetType().Name);
            var messageBus = _serviceProvider.GetRequiredService<IMessageBus<TMessagingClientOptions>>();
            var replyMessage = new Message { Topic = replyResult.ReplyTopic, CorrelationId = replyResult.CorrelationId, Payload = replyResult.Payload.MessagePayloadToJson() };
            replyTasks.Add(messageBus.Publish(replyMessage));
        }
        await Task.WhenAll(replyTasks);
        return result;
    }
}