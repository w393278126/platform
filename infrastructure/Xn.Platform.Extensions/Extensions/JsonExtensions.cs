using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = ServiceStack.Text.JsonSerializer;

namespace Xn.Platform.Core.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings DefaultCamelCaseSettings = new JsonSerializerSettings();
        private static readonly JsonSerializerSettings IgnoreNullValueCamelCaseSettings = new JsonSerializerSettings();
        private static readonly JsonSerializerSettings NoDateFormatCamelCaseSettings = new JsonSerializerSettings();

        static JsonExtensions()
        {
            DefaultCamelCaseSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            DefaultCamelCaseSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            IgnoreNullValueCamelCaseSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            IgnoreNullValueCamelCaseSettings.NullValueHandling = NullValueHandling.Ignore;
            IgnoreNullValueCamelCaseSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            NoDateFormatCamelCaseSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public static string ToCamelCaseJson(this object obj, bool ignoreNullValue = false)
        {
            return ignoreNullValue
                ? JsonConvert.SerializeObject(obj, IgnoreNullValueCamelCaseSettings)
                : JsonConvert.SerializeObject(obj, DefaultCamelCaseSettings);
        }

        public static string ToCamelCaseJsonNotDateFormat(this object obj)
        {
            return JsonConvert.SerializeObject(obj, NoDateFormatCamelCaseSettings);
        }

        public static string ToNewtonsoftJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T ToNewtonsoftObject<T>(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(obj);
        }

        public static string ToJson(this object obj)
        {
            return JsonSerializer.SerializeToString(obj);
        }

        public static T ToObject<T>(this string obj)
        {
            return JsonSerializer.DeserializeFromString<T>(obj);
        }
    }
}