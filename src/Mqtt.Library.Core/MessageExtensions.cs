using Mqtt.Library.Core.Messages;
using Newtonsoft.Json;

namespace Mqtt.Library.Core
{
    public static class MessageExtensions
    {
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
        public static JsonSerializerSettings SerializerSettings() => new();
    }
}