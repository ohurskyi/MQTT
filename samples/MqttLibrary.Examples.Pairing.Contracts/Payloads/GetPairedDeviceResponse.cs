using MessagingLibrary.Core.Messages;

namespace MqttLibrary.Examples.Pairing.Contracts.Payloads;

public class GetPairedDeviceResponse : IMessageResponse
{
    public string DeviceId { get; set; }
        
    public string DeviceName { get; set; }
}