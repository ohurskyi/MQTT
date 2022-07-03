using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class PairDeviceContract : IMessageContract
{
    public string MacAddress { get; set; }
}