using DistributedConfiguration.Contracts.Payloads;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.RequestResponse.Mqtt;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Client;

public class BackgroundRequestSender : BackgroundService
{
    private int _requestCount = 0;
    private readonly IRequestClient<InfrastructureMqttMessagingClientOptions> _requestClient;
    private readonly ILogger<BackgroundRequestSender> _logger;

    public BackgroundRequestSender(IRequestClient<InfrastructureMqttMessagingClientOptions> requestClient, ILogger<BackgroundRequestSender> logger)
    {
        _requestClient = requestClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var payload = new GetPairedDeviceContract { DeviceId = $"Device: {++_requestCount}"};
                var response = await _requestClient.SendAndWaitAsync<GetPairedDeviceResponse>(
                    TopicConstants.RequestUpdate,
                    TopicConstants.ResponseUpdate, payload,
                    TimeSpan.FromSeconds(2));
                _logger.LogInformation("Received response for device with id {value}", response?.DeviceId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error while sending sending {type} request.", typeof(GetPairedDeviceContract));
            }
            
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}