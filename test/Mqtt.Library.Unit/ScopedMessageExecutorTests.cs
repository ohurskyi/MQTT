using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing;
using MessagingLibrary.Processing.Executor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Mqtt.Library.Unit.Handlers;
using Mqtt.Library.Unit.Payloads;
using MQTTnet;
using Xunit;

namespace Mqtt.Library.Unit;

public class UnitTest1
{
    [Fact]
    public async Task ExecuteAsync_CallsMessageHandlingStrategy()
    {
        // // arrange
        // var fixture = new Fixture().Customize(new AutoMoqCustomization());
        //
        // var mqttApplicationMessage = fixture.Create<MqttApplicationMessage>();
        // var mqttApplicationMessageReceivedEventArgs = new MqttApplicationMessageReceivedEventArgs(fixture.Create<string>(), mqttApplicationMessage);
        //
        // var messageHandlingStrategyMock = fixture.Freeze<Mock<IMessageHandlingStrategy>>();
        // var messageHandlerFactoryMock = fixture.Freeze<Mock<IMessageHandlerFactory>>();
        //
        // var serviceProviderMock = fixture.Freeze<Mock<IServiceProvider>>();
        // serviceProviderMock.Setup(s => s.GetService(typeof(IMessageHandlingStrategy))).Returns(messageHandlingStrategyMock.Object);
        //
        // var serviceScopeMock = fixture.Freeze<Mock<IServiceScope>>();
        // serviceScopeMock.Setup(s => s.ServiceProvider).Returns(serviceProviderMock.Object);
        //
        // var serviceScopeFactoryMock = fixture.Freeze<Mock<IServiceScopeFactory>>();
        // serviceScopeFactoryMock.Setup(s => s.CreateScope()).Returns(serviceScopeMock.Object);
        //
        // // act
        // var sut = fixture.Create<ScopedMessageExecutor>();
        // await sut.ExecuteAsync(mqttApplicationMessageReceivedEventArgs);
        //
        // // assert
        // messageHandlingStrategyMock.Verify(x => 
        //     x.Handle(mqttApplicationMessage, messageHandlerFactoryMock.Object, serviceScopeMock.Object), Times.Once);
    }

    [Fact]
    public async Task CallHandlers()
    {
        // arrange
        var builder = new StringBuilder();
        await using var writer = new StringWriter(builder);
        
        var deviceMessagePayload = new DeviceMessagePayload { Name = "Device" };
        var serviceProvider = BuildContainer(writer);

        var factory = serviceProvider.GetRequiredService<IMessageHandlerFactory<TestMessagingClientOptions>>();
        factory.RegisterHandler<HandlerForDeviceNumber1>($"{TopicConstants.DeviceTopic}/{1}");
        factory.RegisterHandler<HandlerForDeviceNumber2>($"{TopicConstants.DeviceTopic}/{2}");
        factory.RegisterHandler<HandlerForAllDeviceNumbers>($"{TopicConstants.DeviceTopic}/#");
        
        // act
        var sut = new ScopedMessageExecutor<TestMessagingClientOptions>(serviceProvider.GetRequiredService<IServiceScopeFactory>());
        await sut.ExecuteAsync(new Message { Topic = $"{TopicConstants.DeviceTopic}/{1}", Payload = deviceMessagePayload.MessagePayloadToJson() });
        var result = builder.ToString().Split(Environment.NewLine);

        // assert
        Assert.Contains("Device Handler 1", result);
        Assert.Contains("Device Handler All", result);

    }
    
    private static IServiceProvider BuildContainer(TextWriter textWriter)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        
        serviceCollection.AddMessagingPipeline<TestMessagingClientOptions>(typeof(HandlerForDeviceNumber1).Assembly);
        serviceCollection.AddSingleton<ITopicFilterComparer, MqttTopicComparer>();
        serviceCollection.AddSingleton(textWriter);
        return serviceCollection.BuildServiceProvider();
    }
}