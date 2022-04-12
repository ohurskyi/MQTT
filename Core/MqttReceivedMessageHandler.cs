using MQTTnet;
using MQTTnet.Client.Receiving;

namespace Mqtt.Library.Test.Core
{
    public class MqttReceivedMessageHandler : IMqttApplicationMessageReceivedHandler
    {
        private readonly IMqttMessageExecutor _messageExecutor;

        public MqttReceivedMessageHandler(IMqttMessageExecutor messageExecutor)
        {
            _messageExecutor = messageExecutor;
        }


        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            await _messageExecutor.ExecuteAsync(eventArgs);
        }
    }
}