using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Messages;
using Newtonsoft.Json;

namespace Mqtt.Library.Core.Extensions
{
    public static class MessageExtensions
    {
        public static T FromJson<T>(this IMessage message) => message.Body.ToObject<T>();

        public static string ToJson(this IMessage message) => JsonConvert.SerializeObject(message.Body, JsonConfigSettings.SerializerSettings());
    }
}