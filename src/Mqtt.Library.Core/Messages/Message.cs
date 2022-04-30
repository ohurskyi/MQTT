namespace Mqtt.Library.Core.Messages;

public class Message: IMessage
{
    public string Topic { get; set; }
    public string Payload { get; set; }
}