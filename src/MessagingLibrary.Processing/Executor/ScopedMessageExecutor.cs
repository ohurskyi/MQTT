using MessagingLibrary.Processing.Strategy;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Messages;

namespace MessagingLibrary.Processing.Executor
{
    public class ScopedMessageExecutor<TMessagingClientOptions> : IMessageExecutor<TMessagingClientOptions> 
        where TMessagingClientOptions: IMessagingClientOptions
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedMessageExecutor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(IMessage message)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlingStrategy = scope.ServiceProvider.GetRequiredService<IMessageHandlingStrategy<TMessagingClientOptions>>();
            await messageHandlingStrategy.Handle(message);
        }
    }
}