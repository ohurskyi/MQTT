# Simple MQTT messaging processing library
MQTT messaging using [MQTTnet](https://github.com/chkr1011/MQTTnet) under the hood with MessagingLibrary Core integration.

## Create handler
```csharp
public class HandlerForDeviceNumber1 : MessageHandlerBase<DeviceMessageContract>
{
    private readonly TextWriter _textWriter;

    public HandlerForDeviceNumber1(TextWriter textWriter)
    {
        _textWriter = textWriter;
    }
    
    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessageContract> messagingContext)
    {
        DeviceMessageContract payload = messagingContext.Payload;
       ...
    }
}
```

## Define messaging options
```csharp
public class InfrastructureMqttMessagingClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } = new() { Host = "infrastructure.com", Port = 1883 };
}
```

## Subscribe
```csharp
var deviceNumberOneTopic = "device/1";
IMessageHandlerFactory<InfrastructureMqttMessagingClientOptions>.RegisterHandler<HandlerForDeviceNumber1>(deviceNumberOneTopic);
```

## Register in DI
```csharp
serviceCollection.AddMqttMessagingPipeline<InfrastructureMqttMessagingClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);
```

## Add MqttClient and Connect with MQTT Messaging Pipeline
```csharp
serviceCollection.AddMqttMessagingClient<InfrastructureMqttMessagingClientOptions>();
serviceProvider.UseMqttMessageReceivedHandler<InfrastructureMqttMessagingClientOptions>();
```

To get easy started please check the samples folder using Distributed Config as an example with multiple handlers and integration events.
