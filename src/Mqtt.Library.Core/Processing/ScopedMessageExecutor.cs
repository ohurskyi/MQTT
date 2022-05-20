using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Strategy;

namespace Mqtt.Library.Core.Processing
{
    public class ScopedMessageExecutor<T> : IMessageExecutor<T> where T : class
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedMessageExecutor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(IMessage message)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlingStrategy = scope.ServiceProvider.GetRequiredService<IMessageHandlingStrategy<T>>();
            await messageHandlingStrategy.Handle(message);
        }
    }
}