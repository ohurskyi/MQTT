using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Results
{
    public class IntegrationEventResult : ExecutionResult
    {
        private IntegrationEventResult(IMessagePayload messagePayload, Type messagingClientOptionsType, string topic)
        {
            Payload = messagePayload;
            MessagingClientOptionsType = messagingClientOptionsType;
            Topic = topic;
        }
        
        public static IntegrationEventResult CreateIntegrationEventResult<TMessagingClientOptions>(IMessagePayload messagePayload, string topic)
            where TMessagingClientOptions : class
        {
            return new IntegrationEventResult(messagePayload, typeof(TMessagingClientOptions), topic);
        }

        public IMessagePayload Payload { get; }

        public Type MessagingClientOptionsType { get; }

        public string Topic { get; set; }
    }
}