using System.Threading.Tasks;
using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Processing.Executor;

namespace MessagingLibrary.Processing.Tests.Clients;

public class InMemoryMessageReceivedHandler<TMessagingClientOptions>
    where TMessagingClientOptions : IMessagingClientOptions
{
    private readonly IMessageExecutor<TMessagingClientOptions> _messageExecutor;
    private readonly InMemoryMessageBusChannel _busChannel;

    public InMemoryMessageReceivedHandler(IMessageExecutor<TMessagingClientOptions> messageExecutor, InMemoryMessageBusChannel busChannel)
    {
        _messageExecutor = messageExecutor;
        _busChannel = busChannel;
    }
    
    public async Task HandleApplicationMessageReceivedAsync()
    {
        var message = await _busChannel.Dequeue();
        await _messageExecutor.ExecuteAsync(message);
    }
}