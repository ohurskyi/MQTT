using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Results
{
    public class IntegrationEventResult : ExecutionResult
    {
        private IntegrationEventResult(IMessagePayload messagePayload, Type messagingClientOptionsType)
        {
            Payload = messagePayload;
            MessagingClientOptionsType = messagingClientOptionsType;
        }
        
        public static IntegrationEventResult CreateIntegrationEventResult<TMessagingClientOptions>(IMessagePayload messagePayload)
            where TMessagingClientOptions : class
        {
            return new IntegrationEventResult(messagePayload, typeof(TMessagingClientOptions));
        }

        public IMessagePayload Payload { get; }

        public Type MessagingClientOptionsType { get; }
    }
}