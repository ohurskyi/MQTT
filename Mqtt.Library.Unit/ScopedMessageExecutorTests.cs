using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Mqtt.Library.Core;
using Mqtt.Library.Processing;
using Mqtt.Library.Processing.Executor;
using MQTTnet;
using Xunit;

namespace Mqtt.Library.Unit;

public class UnitTest1
{
    [Fact]
    public async Task ExecuteAsync_CallsMessageHandlingStrategy()
    {
        // arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        
        var mqttApplicationMessage = fixture.Create<MqttApplicationMessage>();
        var mqttApplicationMessageReceivedEventArgs = new MqttApplicationMessageReceivedEventArgs(fixture.Create<string>(), mqttApplicationMessage);

        var messageHandlingStrategyMock = fixture.Freeze<Mock<IMessageHandlingStrategy>>();
        var messageHandlerFactoryMock = fixture.Freeze<Mock<IMessageHandlerFactory>>();
        
        var serviceProviderMock = fixture.Freeze<Mock<IServiceProvider>>();
        serviceProviderMock.Setup(s => s.GetService(typeof(IMessageHandlingStrategy))).Returns(messageHandlingStrategyMock.Object);
        
        var serviceScopeMock = fixture.Freeze<Mock<IServiceScope>>();
        serviceScopeMock.Setup(s => s.ServiceProvider).Returns(serviceProviderMock.Object);
        
        var serviceScopeFactoryMock = fixture.Freeze<Mock<IServiceScopeFactory>>();
        serviceScopeFactoryMock.Setup(s => s.CreateScope()).Returns(serviceScopeMock.Object);

        // act
        var sut = fixture.Create<ScopedMessageExecutor>();
        await sut.ExecuteAsync(mqttApplicationMessageReceivedEventArgs);
        
        // assert
        messageHandlingStrategyMock.Verify(x => 
            x.Handle(mqttApplicationMessage, messageHandlerFactoryMock.Object, serviceScopeMock.Object), Times.Once);
    }
}