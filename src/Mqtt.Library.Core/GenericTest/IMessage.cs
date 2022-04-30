using Newtonsoft.Json.Linq;

namespace Mqtt.Library.Core.GenericTest;

public interface IMessage
{
    string Topic { get; set; }
    string Payload { get; set; }
}

public class Message: IMessage
{
    public string Topic { get; set; }
    public string Payload { get; set; }
}

public interface IMessagePayload
{
    
}