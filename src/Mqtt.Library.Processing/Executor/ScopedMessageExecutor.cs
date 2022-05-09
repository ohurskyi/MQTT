using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Factory;
using Mqtt.Library.Processing.Extensions;
using MQTTnet;

namespace Mqtt.Library.Processing.Executor
{
    public class ScopedMessageExecutor : IMqttMessageExecutor
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ScopedMessageExecutor> _logger;

        public ScopedMessageExecutor(IMessageHandlerFactory messageHandlerFactory, IServiceScopeFactory serviceScopeFactory, ILogger<ScopedMessageExecutor> logger)
        {
            _messageHandlerFactory = messageHandlerFactory;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs)
        {
            _logger.LogInformation("Begin processing mqtt message ...");
            
            using var scope = _serviceScopeFactory.CreateScope();
            var messageHandlerWrapper = scope.ServiceProvider.GetRequiredService<IMessageHandlingStrategy>();
            var handlerFactory = scope.ServiceProvider.GetRequiredService<HandlerFactory>();
            var message = messageReceivedEventArgs.ApplicationMessage.ToMessage();
            await messageHandlerWrapper.Handle(message, _messageHandlerFactory, handlerFactory);
            
            _logger.LogInformation("End processing mqtt message.");
        }
    }
}