using MessagingLibrary.Core.Handlers;

namespace Mqtt.Library.Unit.Handlers;

public class DeviceMessageHandlerDefinition<TMessageHandler> : MessageHandlerDefinition<TMessageHandler>
    where TMessageHandler : class, IMessageHandler
{
    public DeviceMessageHandlerDefinition() : base($"{TopicConstants.DeviceTopic}/#")
    {
        
    }
    
    public DeviceMessageHandlerDefinition(int deviceNumber) : base($"{TopicConstants.DeviceTopic}/{deviceNumber}")
    {
    }
}