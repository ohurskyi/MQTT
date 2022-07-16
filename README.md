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
public class InMemoryClientOptions : IMessagingClientOptions
{
}
```

## Subscribe
```csharp
var deviceNumberOneTopic = "device/1";
IMessageHandlerFactory<InMemoryClientOptions>.RegisterHandler<HandlerForDeviceNumber1>(deviceNumberOneTopic);
```

## Register in DI
```csharp
serviceCollection.AddMessagingPipeline<InMemoryClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);
```

To get started please check the samples folder using Distributed Config as an example with multiple handlers and integration events.
