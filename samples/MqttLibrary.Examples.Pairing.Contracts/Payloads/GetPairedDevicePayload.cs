using Mqtt.Library.Core.Messages;

namespace MqttLibrary.Examples.Pairing.Contracts.Payloads;

public class GetPairedDevicePayload : IMessagePayload
{
    public string DeviceId { get; set; }
}