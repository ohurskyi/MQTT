using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Processing.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Tests.Clients;
using MessagingLibrary.Processing.Tests.Contracts;
using MessagingLibrary.Processing.Tests.Handlers;
using MessagingLibrary.Processing.Tests.Options;
using MessagingLibrary.Processing.Tests.Topics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace MessagingLibrary.Processing.Tests;

public class MessageBusTests
{
    [Fact]
    public async Task Test()
    {
        // arrange
        var builder = new StringBuilder();
        await using var writer = new StringWriter(builder);
        var serviceProvider = BuildContainer(writer);
        
        var multiWildCardDeviceTopic = $"{DeviceTopicConstants.DeviceTopic}/#";
        var factory = serviceProvider.GetRequiredService<IMessageHandlerFactory<TestMessagingClientOptions>>();
        factory.RegisterHandler<HandlerForAllDeviceNumbers>(multiWildCardDeviceTopic);
        
        var contract =  new DeviceMessageContract { Name = "Device" };
        var publishTopic = $"{DeviceTopicConstants.DeviceTopic}/{1}";
        var message = new Message { Topic = publishTopic, Payload = contract.MessagePayloadToJson() };

        // act
        var messageBus = serviceProvider.GetRequiredService<IMessageBus<TestMessagingClientOptions>>();
        await messageBus.Publish(message);
        var messageReceivedHandler = serviceProvider.GetRequiredService<InMemoryMessageReceivedHandler<TestMessagingClientOptions>>();
        await messageReceivedHandler.HandleApplicationMessageReceivedAsync();
        var result = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        // assert
        Assert.Contains("Device " + nameof(HandlerForAllDeviceNumbers), result);
    }
    
    
    private static IServiceProvider BuildContainer(TextWriter textWriter)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        serviceCollection.AddSingleton<IMessageBus<TestMessagingClientOptions>, InMemoryMessageBus<TestMessagingClientOptions>>();
        serviceCollection.AddSingleton<InMemoryMessageBusChannel>();
        serviceCollection.AddSingleton<InMemoryMessageReceivedHandler<TestMessagingClientOptions>>();
        
        serviceCollection.AddMessageHandlers(typeof(HandlerForDeviceNumber1).Assembly);
        serviceCollection.AddMessagingPipeline<TestMessagingClientOptions>();
        serviceCollection.AddMqttTopicComparer();
        serviceCollection.AddSingleton(textWriter);
        return serviceCollection.BuildServiceProvider();
    }
}