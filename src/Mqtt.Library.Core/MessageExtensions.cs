using Mqtt.Library.Core.Messages;
using Newtonsoft.Json;

namespace Mqtt.Library.Core
{
    public static class MessageExtensions
    {
        public static T FromJson<T>(this IMessage message) => message.Body.ToObject<T>();

        public static string ToJson(this IMessage message) => JsonConvert.SerializeObject(message.Body, JsonConfigSettings.SerializerSettings());
    }
    
    public static class JsonConfigSettings
    {
        public static JsonSerializerSettings SerializerSettings() => new();
    }
}