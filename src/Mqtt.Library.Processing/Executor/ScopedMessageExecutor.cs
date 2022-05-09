using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Processing.Executor
{
    public class ScopedMessageExecutor : IMessageExecutor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedMessageExecutor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(IMessage message)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlingStrategy = scope.ServiceProvider.GetRequiredService<IMessageHandlingStrategy>();
            await messageHandlingStrategy.Handle(message);
        }
    }
}