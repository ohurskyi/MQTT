using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using Microsoft.Extensions.Hosting;

namespace MessagingLibrary.TopicClient.Mqtt;

public class MessageConsumersHostedService : IHostedService
{
    private readonly IConsumerDefinitionListenerProvider _consumerDefinitionListenerProvider;
    
    public MessageConsumersHostedService(IConsumerDefinitionListenerProvider consumerDefinitionListenerProvider)
    {
        _consumerDefinitionListenerProvider = consumerDefinitionListenerProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var listeners = _consumerDefinitionListenerProvider.Listeners;
        await Task.WhenAll(listeners.Select(listener => listener.StartListening()));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var listeners = _consumerDefinitionListenerProvider.Listeners;
        await Task.WhenAll(listeners.Select(listener => listener.StopListening()));
    }
}