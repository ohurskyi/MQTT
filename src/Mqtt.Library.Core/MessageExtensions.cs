using Mqtt.Library.Core.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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