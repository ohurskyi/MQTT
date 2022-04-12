using MqttClientTest.Messaging.Processing;
using MQTTnet;

namespace Mqtt.Library.Test.Core
{
    public class MqttMessageExecutor : IMqttMessageExecutor
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MqttMessageExecutor(IMessageHandlerFactory messageHandlerFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _messageHandlerFactory = messageHandlerFactory;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlerWrapper = scope.ServiceProvider.GetRequiredService<MessageHandlerWrapper>();
            var mqttApplicationMessage = messageReceivedEventArgs.ApplicationMessage;
            await messageHandlerWrapper.Handle(mqttApplicationMessage, _messageHandlerFactory, scope.ServiceProvider);
        }
    }
}