using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Core.Results
{
    public class IntegrationEventResult : ExecutionResult
    {
        public string Topic { get; }
        public IMessagePayload Payload { get; }

        public IntegrationEventResult(IMessagePayload payload, string topic)
        {
            Topic = topic;
            Payload = payload;
        }
    }
}