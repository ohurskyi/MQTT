using MessagingLibrary.Core.Messages;

namespace MqttLibrary.Examples.Contracts.Payloads;

public class DeviceMessagePayload : IMessagePayload
{
    public string Name { get; set; }
}