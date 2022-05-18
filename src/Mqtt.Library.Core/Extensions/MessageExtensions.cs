using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Messages;
using Newtonsoft.Json.Linq;

namespace Mqtt.Library.Core.Extensions
{
    public static class MessageExtensions
    {
        public static T FromJson<T>(this IMessage message) => message.Body.ToObject<T>(JsonConfiguration.JsonSerializer);

        public static JObject ToJsonObject(this object @object)
        {
            return JObject.FromObject(@object, JsonConfiguration.JsonSerializer);
        }
    }
}