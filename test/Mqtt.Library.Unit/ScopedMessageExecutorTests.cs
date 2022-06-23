using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing;
using MessagingLibrary.Processing.Executor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Mqtt.Library.Processing;
using Mqtt.Library.Unit.Handlers;
using Mqtt.Library.Unit.Payloads;
using Xunit;

namespace Mqtt.Library.Unit;

public class UnitTest1
{
    [Fact]
    public async Task ExecuteAsync_ForDeviceNumberOneTopic_CallsHandlerForOneAndForAll()
    {
        // arrange
        var builder = new StringBuilder();
        await using var writer = new StringWriter(builder);
        
        var deviceMessagePayload = new DeviceMessagePayload { Name = "Device" };
        var serviceProvider = BuildContainer(writer);

        var factory = serviceProvider.GetRequiredService<IMessageHandlerFactory<TestMessagingClientOptions>>();
        factory.RegisterHandler<HandlerForDeviceNumber1>(BuildDeviceTopic(1));
        factory.RegisterHandler<HandlerForDeviceNumber2>(BuildDeviceTopic(1));
        factory.RegisterHandler<HandlerForAllDeviceNumbers>(GetAllDevicesTopic());
        
        // act
        var sut = new ScopedMessageExecutor<TestMessagingClientOptions>(serviceProvider.GetRequiredService<IServiceScopeFactory>());
        await sut.ExecuteAsync(new Message { Topic = $"{TopicConstants.DeviceTopic}/{1}", Payload = deviceMessagePayload.MessagePayloadToJson() });
        var result = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        // assert
        Assert.Contains("Device " + nameof(HandlerForDeviceNumber1), result);
        Assert.Contains("Device " + nameof(HandlerForAllDeviceNumbers), result);
    }
    
    private static IServiceProvider BuildContainer(TextWriter textWriter)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        
        serviceCollection.AddMessagingPipeline<TestMessagingClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);
        serviceCollection.AddMqttTopicComparer();
        serviceCollection.AddSingleton(textWriter);
        return serviceCollection.BuildServiceProvider();
    }

    private static string GetAllDevicesTopic()
    {
        return $"{TopicConstants.DeviceTopic}/#";
    }

    private static string BuildDeviceTopic(int deviceNumber)
    {
        return $"{TopicConstants.DeviceTopic}/{deviceNumber}";
    }
}