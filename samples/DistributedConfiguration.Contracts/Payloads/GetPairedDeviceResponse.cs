using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class GetPairedDeviceResponse : IMessageResponse
{
    public string DeviceId { get; set; }
        
    public string DeviceName { get; set; }
}