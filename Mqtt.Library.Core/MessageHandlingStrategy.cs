using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQTTnet;

namespace Mqtt.Library.Core;

public class MessageHandlingStrategy : IMessageHandlingStrategy, IDisposable
{
    private readonly ILogger<MessageHandlingStrategy> _logger;

    public MessageHandlingStrategy(ILogger<MessageHandlingStrategy> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MqttApplicationMessage mqttApplicationMessage, IMessageHandlerFactory messageHandlerFactory, IServiceScope serviceScope)
    {
        var handlers = messageHandlerFactory.GetHandlers(mqttApplicationMessage.Topic, serviceScope.ServiceProvider.GetRequiredService);

        var funcs = handlers.Select(h => new Func<MqttApplicationMessage, Task>(h.Handle));

        await HandleStrategy(funcs, mqttApplicationMessage);
    }

    protected virtual async Task HandleStrategy(IEnumerable<Func<MqttApplicationMessage, Task>> handlers, MqttApplicationMessage mqttApplicationMessage)
    {
        foreach (var handler in handlers)
        {
            await handler(mqttApplicationMessage);
        }
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(MessageHandlingStrategy)} disposed.");
    }
}