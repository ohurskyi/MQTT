using Mqtt.Library.Core.Messages;

namespace MqttLibrary.Examples.Pairing.Contracts.Payloads;

public class PairDevicePayload : IMessagePayload
{
    public string MacAddress { get; set; }
}