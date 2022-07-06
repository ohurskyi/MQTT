using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Pairing;

public class GetPairedDeviceContract : IMessageContract
{
    public string DeviceId { get; set; }
}