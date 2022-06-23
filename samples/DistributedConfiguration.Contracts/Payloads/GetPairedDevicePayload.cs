using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class GetPairedDevicePayload : IMessagePayload
{
    public string DeviceId { get; set; }
}