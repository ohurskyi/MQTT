using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mqtt.Library.Core.Configuration;

public static class JsonConfiguration
{
    public static readonly JsonSerializer JsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    });
}