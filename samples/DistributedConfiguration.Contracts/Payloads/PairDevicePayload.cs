using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class PairDevicePayload : IMessagePayload
{
    public string MacAddress { get; set; }
}