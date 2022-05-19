using Mqtt.Library.Core.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mqtt.Library.Core.Extensions
{
    public static class MessageExtensions
    {
        public static string MessagePayloadToJson<T>(this T messagePayload) where T: IMessagePayload
        {
            return JsonConvert.SerializeObject(messagePayload, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        
        public static T MessagePayloadFromJson<T>(this string messagePayload) where T: IMessagePayload
        {
            return JsonConvert.DeserializeObject<T>(messagePayload, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}