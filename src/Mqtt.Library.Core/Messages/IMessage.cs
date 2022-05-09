namespace Mqtt.Library.Core.Messages;

public interface IMessage
{
    string Topic { get; set; }
    string Payload { get; set; }
    object Body { get; set; }
}