using System.Threading.Tasks;
using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Processing.Tests.Clients;

public class InMemoryMessageBus<TMessagingClientOptions> : IMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMessagingClientOptions
{
    private readonly InMemoryMessageBusChannel _busChannel;

    public InMemoryMessageBus(InMemoryMessageBusChannel busChannel)
    {
        _busChannel = busChannel;
    }

    public Task Publish(IMessageContract contract, string topic)
    {
        throw new System.NotImplementedException();
    }

    public async Task Publish(IMessage message)
    {
        await _busChannel.Enqueue(message);
    }
}