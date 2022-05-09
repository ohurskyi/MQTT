using Newtonsoft.Json.Linq;

namespace Mqtt.Library.Core.Messages;

public interface IMessage
{
    string Topic { get; set; }
    string Payload { get; set; }
    JObject Body { get; set; }
}