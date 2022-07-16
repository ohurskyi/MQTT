using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Executor;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Tests.Contracts;
using MessagingLibrary.Processing.Tests.Handlers;
using MessagingLibrary.Processing.Tests.Options;
using MessagingLibrary.Processing.Tests.Topics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace MessagingLibrary.Processing.Tests;

public class ScopedMessageExecutorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(69)]
    [InlineData(420)]
    public async Task ExecuteAsync_MultiWildCardDeviceTopic_CallHandlerForAllDevices(int deviceNumber)
    {
        // arrange
        var builder = new StringBuilder();
        await using var writer = new StringWriter(builder);
        var serviceProvider = BuildContainer(writer);

        var multiWildCardDeviceTopic = $"{DeviceTopicConstants.DeviceTopic}/#";
        var factory = serviceProvider.GetRequiredService<IMessageHandlerFactory<TestMessagingClientOptions>>();
        factory.RegisterHandler<HandlerForAllDeviceNumbers>(multiWildCardDeviceTopic);

        var contract =  new DeviceMessageContract { Name = "Device" };
        var publishTopic = $"{DeviceTopicConstants.DeviceTopic}/{deviceNumber}";
        var message = new Message { Topic = publishTopic, Payload = contract.MessagePayloadToJson() };

        // act
        var sut = new ScopedMessageExecutor<TestMessagingClientOptions>(serviceProvider.GetRequiredService<IServiceScopeFactory>());
        await sut.ExecuteAsync(message);
        var result = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        // assert
        Assert.Contains("Device " + nameof(HandlerForAllDeviceNumbers), result);
    }
    
    [Fact]
    public async Task ExecuteAsync_ForDeviceNumberOneTopic_CallsHandlerForDeviceNumber1()
    {
        // arrange
        var builder = new StringBuilder();
        await using var writer = new StringWriter(builder);
        var serviceProvider = BuildContainer(writer);
        
        const int deviceNumberOne = 1;
        const int deviceNumberTwo = 2;
        var factory = serviceProvider.GetRequiredService<IMessageHandlerFactory<TestMessagingClientOptions>>();
        factory.RegisterHandler<HandlerForDeviceNumber1>($"{DeviceTopicConstants.DeviceTopic}/{deviceNumberOne}");
        factory.RegisterHandler<HandlerForDeviceNumber2>($"{DeviceTopicConstants.DeviceTopic}/{deviceNumberTwo}");

        var contract =  new DeviceMessageContract { Name = "Device" };
        var publishTopic = $"{DeviceTopicConstants.DeviceTopic}/{deviceNumberOne}";
        var message = new Message { Topic = publishTopic, Payload = contract.MessagePayloadToJson() };

        // act
        var sut = new ScopedMessageExecutor<TestMessagingClientOptions>(serviceProvider.GetRequiredService<IServiceScopeFactory>());
        await sut.ExecuteAsync(message);
        var result = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        // assert
        Assert.Contains("Device " + nameof(HandlerForDeviceNumber1), result);
        Assert.DoesNotContain("Device " + nameof(HandlerForDeviceNumber2), result);
    }
    
    [Fact]
    public async Task ExecuteAsync_SingleLevelWildCardDeviceTopic_CallsHandlerForAllDeviceNumbers()
    {
        // arrange
        var builder = new StringBuilder();
        await using var writer = new StringWriter(builder);
        var serviceProvider = BuildContainer(writer);
        
        const int deviceNumberOne = 1;
        const int deviceNumberTwo = 2;
        var singleLevelWildCardTopic = $"{DeviceTopicConstants.DeviceTopic}/+/temperature";

        var factory = serviceProvider.GetRequiredService<IMessageHandlerFactory<TestMessagingClientOptions>>();
        factory.RegisterHandler<HandlerForDeviceNumber1>($"{DeviceTopicConstants.DeviceTopic}/{deviceNumberOne}/brightness");
        factory.RegisterHandler<HandlerForDeviceNumber2>($"{DeviceTopicConstants.DeviceTopic}/{deviceNumberTwo}/brightness");
        factory.RegisterHandler<HandlerForAllDeviceNumbers>(singleLevelWildCardTopic);

        var contract =  new DeviceMessageContract { Name = "Device" };
        var publishTopic = $"{DeviceTopicConstants.DeviceTopic}/{deviceNumberOne}/temperature";
        var message = new Message { Topic = publishTopic, Payload = contract.MessagePayloadToJson() };
        
        // act
        var sut = new ScopedMessageExecutor<TestMessagingClientOptions>(serviceProvider.GetRequiredService<IServiceScopeFactory>());
        await sut.ExecuteAsync(message);
        var result = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        // assert
        Assert.Contains("Device " + nameof(HandlerForAllDeviceNumbers), result);
        Assert.DoesNotContain("Device " + nameof(HandlerForDeviceNumber1), result);
        Assert.DoesNotContain("Device " + nameof(HandlerForDeviceNumber2), result);
    }
    
    private static IServiceProvider BuildContainer(TextWriter textWriter)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        serviceCollection.AddMessageHandlers(typeof(HandlerForDeviceNumber1).Assembly);
        serviceCollection.AddMessagingPipeline<TestMessagingClientOptions>();
        serviceCollection.AddMqttTopicComparer();
        serviceCollection.AddSingleton(textWriter);
        return serviceCollection.BuildServiceProvider();
    }
}