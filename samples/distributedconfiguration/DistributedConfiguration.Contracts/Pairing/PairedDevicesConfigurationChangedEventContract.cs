using DistributedConfiguration.Contracts.Models;
using MessagingLibrary.Core.Messages;

namespace DistributedConfiguration.Contracts.Pairing;

public class PairedDevicesConfigurationChangedEventContract : IMessageContract
{
    public PairedDevicesModel PairedDevicesModel { get; set; }
}