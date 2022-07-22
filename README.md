# Simple MQTT message processing library

MQTT message processing is done using [MessagingLibrary Core/Processing](https://github.com/ohurskyi/MQTT/tree/main/src/MessagingLibrary) with integration of [MQTTnet](https://github.com/chkr1011/MQTTnet).

MessagingLibrary Core has no dependencies. Integration with MQTT done in separate proj [MessagingLibrary Mqtt](https://github.com/ohurskyi/MQTT/tree/main/src/MessagingLibrary.Mqtt).

## Create Message Contract
```csharp
public class DeviceMessageContract : IMessageContract
{
    public string Name { get; set; }
}
```

## Create handler
```csharp
public class DeviceHandler : MessageHandlerBase<DeviceMessageContract>
{
    private readonly TextWriter _textWriter;

    public DeviceHandler(TextWriter textWriter)
    {
        _textWriter = textWriter;
    }
    
    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessageContract> messagingContext)
    {
        DeviceMessageContract payload = messagingContext.Payload;
        await _textWriter.WriteLineAsync("Received device with name: " + payload.Name);
        return new SuccessfulResult();
    }
}
```

## Define messaging options
each new client should have its own implementation of ```IMqttMessagingClientOptions```
```csharp
public class InfrastructureClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } = new() { Host = "infrastructure.dev.com", Port = 1883 };
}
```

## Consumers
map handler(s) to a topic
```csharp
public class DeviceConsumerDefinition : IConsumerDefinition
{
    private readonly List<int> _availableDeviceNumbers;

    public DeviceConsumerDefinition(List<int> availableDeviceNumbers)
    {
        _availableDeviceNumbers = availableDeviceNumbers;
    }

    public IEnumerable<ISubscriptionDefinition> Definitions()
    {
        return _availableDeviceNumbers.Select(deviceNumber => new SubscriptionDefinition<DeviceHandler>($"device/{deviceNumber}"));
    }
}
```
aggregate one/multiple definitions in a provider
```csharp
public class ConsumerDefinitionProvider : IConsumerDefinitionProvider
{
    private readonly IAvailableDevicesConfig _availableDevicesConfig;

    public ConsumerDefinitionProvider(IAvailableDevicesConfig availableDevicesConfig)
    {
        _availableDevicesConfig = availableDevicesConfig;
    }

    public IEnumerable<IConsumerDefinition> Definitions => new List<IConsumerDefinition>
    {
        new DeviceConsumerDefinition(_availableDevicesConfig.GetDevices())
    };
}
```
create listener, so subscriptions will be created on startup and removed on tear down of the host.
```csharp
public class ConsumerDefinitionListenerProvider : IConsumerDefinitionListenerProvider
{
    private readonly ITopicClient<InfrastructureClientOptions> _topicClient;

    public ConsumerDefinitionListenerProvider(ITopicClient<InfrastructureClientOptions> topicClient)
    {
        _topicClient = topicClient;
    }

    public IEnumerable<IConsumerListener> Listeners => new List<IConsumerListener>
    {
        new ConsumerListener<InfrastructureClientOptions>(_topicClient, new ConsumerDefinitionProvider())
    };
}
```
if you want dynamically map the handler to the topic
```csharp
private readonly ITopicClient<InfrastructureClientOptions> _topicClient;
...

if (configurationSource == "external")
{
    var deviceTopicName = $"devices/remote/{remoteDeviceNumber}";
    _topicClient.Subscribe(new SubscriptionDefinition<DeviceHandler>(deviceTopicName));
}
...
```

## Register in DI
```csharp
public static IServiceCollection AddDeviceDomainServices(this IServiceCollection serviceCollection)
{
    serviceCollection.AddMessageHandlers(typeof(DeviceHandler).Assembly);
    serviceCollection.AddConsumerDefinitionListenerProvider<ConsumerDefinitionListenerProvider>();
    serviceCollection.AddMessageConsumersHostedService();
}
```
## Append message processing to specific client
```csharp
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, serviceCollection) =>
        {
            var configuration = hostContext.Configuration;
            
            serviceCollection.AddMqttMessagingClient<InfrastructureClientOptions>(configuration);

            serviceCollection.AddMqttTopicClient<InfrastructureClientOptions>();

            serviceCollection.AddMqttMessageBus<InfrastructureClientOptions>();

            serviceCollection.AddMqttPipe<InfrastructureClientOptions>();

            serviceCollection.AddDeviceDomainServices();

        })
...

host.Services.UseMqttMessageReceivedHandler<InfrastructureClientOptions>();
```

To get started please check the [samples](https://github.com/ohurskyi/MQTT/tree/main/samples/distributedconfiguration) as an example with multiple handlers and integration events.
