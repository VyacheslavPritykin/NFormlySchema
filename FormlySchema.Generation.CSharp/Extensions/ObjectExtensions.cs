using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FormlySchema.Generation.CSharp
{
    internal static class ObjectExtensions
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string SerializeToJson(this object instance, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(instance, formatting, (JsonSerializerSettings?) JsonSerializerSettings);
        }

        public static bool HasData(this object instance)
        {
            return instance.SerializeToJson(Formatting.None) != "{}";
        }
    }
}