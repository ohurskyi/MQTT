using System.Threading.Channels;
using System.Threading.Tasks;
using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Processing.Tests.Clients;

public class InMemoryMessageBusChannel
{
    private readonly Channel<IMessage> _channel = Channel.CreateUnbounded<IMessage>();

    public async Task Enqueue(IMessage message)
    {
        await _channel.Writer.WriteAsync(message);
    }

    public async Task<IMessage> Dequeue()
    {
        return await _channel.Reader.ReadAsync();
    }
}