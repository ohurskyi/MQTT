using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class PairedDevicesConfigurationChangedEventPayload : IMessagePayload
{
    public PairedDevices PairedDevices { get; set; }
}