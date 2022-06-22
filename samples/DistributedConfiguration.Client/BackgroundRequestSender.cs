using Mqtt.Library.Client.Local;
using Mqtt.Library.RequestResponse;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;
using MqttLibrary.Examples.Pairing.Contracts.Topics;

namespace DistributedConfiguration.Client;

public class BackgroundRequestSender : BackgroundService
{
    private int _requestCount = 0;
    private readonly IRequestClient<LocalMqttMessagingClientOptions> _requestClient;
    private readonly ILogger<BackgroundRequestSender> _logger;

    public BackgroundRequestSender(IRequestClient<LocalMqttMessagingClientOptions> requestClient, ILogger<BackgroundRequestSender> logger)
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
                var payload = new GetPairedDevicePayload { DeviceId = $"Device: {++_requestCount}"};
                var response = await _requestClient.SendAndWaitAsync<GetPairedDeviceResponse>(
                    TopicConstants.RequestUpdate,
                    TopicConstants.ResponseUpdate, payload,
                    TimeSpan.FromSeconds(2));
                _logger.LogInformation("Received response for device with id {value}", response?.DeviceId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error while sending sending {type} request.", typeof(GetPairedDevicePayload));
            }
            
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}