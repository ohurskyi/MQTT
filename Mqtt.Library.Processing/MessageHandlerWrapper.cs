using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using MQTTnet;

namespace Mqtt.Library.Processing;

public class MessageHandlerWrapper : IDisposable
{
    private readonly ILogger<MessageHandlerWrapper> _logger;

    public MessageHandlerWrapper(ILogger<MessageHandlerWrapper> logger)
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
        _logger.LogInformation($"{nameof(MessageHandlerWrapper)} disposed.");;
    }
}