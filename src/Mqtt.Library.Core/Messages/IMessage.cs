using Newtonsoft.Json.Linq;

namespace Mqtt.Library.Core.Messages;

public interface IMessage
{
    string Topic { get; set; }
    string ReplyTopic { get; set; }
    string Payload { get; set; }
    Guid CorrelationId { get; set; }
    JObject Body { get; set; }
}