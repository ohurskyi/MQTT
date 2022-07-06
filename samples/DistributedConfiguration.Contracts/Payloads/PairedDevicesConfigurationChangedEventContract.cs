using DistributedConfiguration.Contracts.Models;
using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Payloads;

public class PairedDevicesConfigurationChangedEventContract : IMessageContract
{
    public PairedDevicesModel PairedDevicesModel { get; set; }
}