using Newtonsoft.Json;

namespace Mqtt.Library.Core.Configuration;

public static class JsonConfigSettings
{
    public static JsonSerializerSettings SerializerSettings() => new();
}