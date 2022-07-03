using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class PairedDevicesConfigurationChangedEventContract : IMessageContract
{
    public PairedDevices PairedDevices { get; set; }
}