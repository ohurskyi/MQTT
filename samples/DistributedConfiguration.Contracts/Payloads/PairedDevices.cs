namespace DistributedConfiguration.Contracts.Payloads;

public class PairedDevices
{
    public IReadOnlyCollection<string> DeviceMacAddresses { get; set; }
}