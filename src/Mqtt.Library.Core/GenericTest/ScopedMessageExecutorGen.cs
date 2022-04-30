using Microsoft.Extensions.DependencyInjection;
using MQTTnet;

namespace Mqtt.Library.Core.GenericTest
{
    public class ScopedMessageExecutorGen 
    {
        private readonly IMessageHandlerFactoryGen _messageHandlerFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedMessageExecutorGen(IMessageHandlerFactoryGen messageHandlerFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _messageHandlerFactory = messageHandlerFactory;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlerWrapper = scope.ServiceProvider.GetRequiredService<MessageHandlingStrategyGen>();
            var message = messageReceivedEventArgs.ApplicationMessage.ToMessage();
            await messageHandlerWrapper.Handle(message, _messageHandlerFactory, scope);
        }
    }
}