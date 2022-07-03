using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class GetPairedDeviceContract : IMessageContract
{
    public string DeviceId { get; set; }
}