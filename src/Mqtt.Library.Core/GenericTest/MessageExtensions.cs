using System.Text;
using MQTTnet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Mqtt.Library.Core.GenericTest
{
    public static class MessageExtensions
    {
        public static IMessage ToMessage(this MqttApplicationMessage mqttApplicationMessage)
        {
            var payloadStr = Encoding.UTF8.GetString(mqttApplicationMessage.Payload);
            var message = new Message { Topic = mqttApplicationMessage.Topic, Payload = payloadStr };
            return message;
        }
        
        public static MqttApplicationMessage ToMqttMessage(this IMessage msg)
        {
            var mqttApplicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(msg.Topic)
                .WithPayload(msg.Payload)
                .Build();

            return mqttApplicationMessage;
        }

        public static T FromJson<T>(this IMessage message)
        {
            return JsonConvert.DeserializeObject<T>(message.Payload, JsonConfigSettings.SerializerSettings());
        }
        
        public static string ToJson(this IMessagePayload messagePayload)
        {
            return JsonConvert.SerializeObject(messagePayload, JsonConfigSettings.SerializerSettings());
        }
    }
    
    public static class JsonConfigSettings
    {
        public static JsonSerializerSettings SerializerSettings() => new JsonSerializerSettings
        {
            // TypeNameHandling = TypeNameHandling.Objects,
            // SerializationBinder = new KnownTypesBinder { KnownTypes = new List<Type>() }
        };
    }
    
    public class KnownTypesBinder : ISerializationBinder
    {
        public IList<Type> KnownTypes { get; set; }

        public Type BindToType(string assemblyName, string typeName)
        {
            return KnownTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}