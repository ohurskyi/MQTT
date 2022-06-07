using MessagingLibrary.Core.Messages;

namespace MqttLibrary.Examples.Pairing.Contracts.Payloads;

public class PairedDevicesConfigurationChangedEventPayload : IMessagePayload
{
    public PairedDevices PairedDevices { get; set; }
}