using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core;
using MQTTnet;

namespace Mqtt.Library.Processing.Executor
{
    public class ScopedMessageExecutor : IMqttMessageExecutor
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedMessageExecutor(IMessageHandlerFactory messageHandlerFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _messageHandlerFactory = messageHandlerFactory;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlerWrapper = scope.ServiceProvider.GetRequiredService<IMessageHandlingStrategy>();
            var mqttApplicationMessage = messageReceivedEventArgs.ApplicationMessage;
            await messageHandlerWrapper.Handle(mqttApplicationMessage, _messageHandlerFactory, scope);
        }
    }
}