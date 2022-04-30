using Microsoft.Extensions.DependencyInjection;

namespace Mqtt.Library.Core.GenericTest;

public class MessageHandlingStrategyGen
{
    public async Task Handle(IMessage message, IMessageHandlerFactoryGen messageHandlerFactory, IServiceScope serviceScope)
    {
        var handlers = messageHandlerFactory.GetHandlers(message.Topic, serviceScope.ServiceProvider.GetRequiredService);
        
        var funcs = handlers.Select(h => new Func<IMessage, Task>(h.Handle));
        
        await HandleStrategy(funcs, message);
    }

    protected virtual async Task HandleStrategy(IEnumerable<Func<IMessage, Task>> handlers, IMessage message)
    {
        foreach (var handler in handlers)
        {
            await handler(message);
        }
    }
}