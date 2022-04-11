using MqttClientTest.Messaging.Processing;
using MQTTnet;

namespace Mqtt.Library.Test.Core
{
    public class MqttMessageExecutor : IMqttMessageExecutor
    {
        private readonly MessageHandlerWrapper _messageHandlerWrapper;
        private readonly IMessageHandlerFactory _messageHandlerFactory;

        public MqttMessageExecutor(MessageHandlerWrapper messageHandlerWrapper, IMessageHandlerFactory messageHandlerFactory)
        {
            _messageHandlerWrapper = messageHandlerWrapper;
            _messageHandlerFactory = messageHandlerFactory;
        }

        public async Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs)
        {
            var mqttApplicationMessage = messageReceivedEventArgs.ApplicationMessage;
            await _messageHandlerWrapper.Handle(mqttApplicationMessage, _messageHandlerFactory);
        }
    }
}