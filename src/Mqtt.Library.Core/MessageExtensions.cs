using Mqtt.Library.Core.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mqtt.Library.Core
{
    public static class MessageExtensions
    {
        public static T FromJson<T>(this IMessage message)
        {
            var jObject = message.Body;
            return jObject.ToObject<T>();
        }

        public static string ToJson(this IMessage message)
        {
            return JsonConvert.SerializeObject(message.Body, JsonConfigSettings.SerializerSettings());
        }
    }
    
    public static class JsonConfigSettings
    {
        public static JsonSerializerSettings SerializerSettings() => new();
    }
}