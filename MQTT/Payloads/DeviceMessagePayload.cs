using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Test.Payloads;

public class DeviceMessagePayload : IMessagePayload
{
    public string Name { get; set; }
}