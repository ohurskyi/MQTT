using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Factory;
using MQTTnet;

namespace Mqtt.Library.Processing.Executor
{
    public class ScopedMessageExecutorGen : IMqttMessageExecutor
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedMessageExecutorGen(IMessageHandlerFactory messageHandlerFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _messageHandlerFactory = messageHandlerFactory;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlerWrapper = scope.ServiceProvider.GetRequiredService<IMessageHandlingStrategy>();
            var message = messageReceivedEventArgs.ApplicationMessage.ToMessage();
            await messageHandlerWrapper.Handle(message, _messageHandlerFactory, scope);
        }
    }
}