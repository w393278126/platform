using MsgPack.Serialization;
using Xn.Platform.Core.Extensions;
using System.IO;

namespace System.Web.Mvc
{
    /// <summary>
    /// JSONP封装类
    /// </summary>
    public class JsonpResult : JsonpStreamResult
    {
        public static readonly JsonpResult EmptyResult = new JsonpResult("[]");
        public static readonly JsonpResult EmptyObjectResult = new JsonpResult("{}");
        public static readonly JsonpResult EmptyValue = new JsonpResult("");


        private static readonly SerializationContext DefaultSerializationContext;
        private static readonly SerializationContext CamelCaseSerializationContext;

        static JsonpResult()
        {
            DefaultSerializationContext = new SerializationContext();
            DefaultSerializationContext.SerializationMethod = SerializationMethod.Map;
            DefaultSerializationContext.EnumSerializationMethod = EnumSerializationMethod.ByUnderlyingValue;
            DefaultSerializationContext.DefaultDateTimeConversionMethod = DateTimeConversionMethod.UnixEpoc;
            DefaultSerializationContext.DictionarySerlaizationOptions.OmitNullEntry = true;
            CamelCaseSerializationContext = new SerializationContext();
            CamelCaseSerializationContext.SerializationMethod = SerializationMethod.Map;
            CamelCaseSerializationContext.EnumSerializationMethod = EnumSerializationMethod.ByUnderlyingValue;
            CamelCaseSerializationContext.DefaultDateTimeConversionMethod = DateTimeConversionMethod.UnixEpoc;
            CamelCaseSerializationContext.DictionarySerlaizationOptions.OmitNullEntry = true;
            CamelCaseSerializationContext.DictionarySerlaizationOptions.KeyTransformer = DictionaryKeyTransformers.LowerCamel;
        }

        public string Data
        {
            get
            {
                switch (_serializationType)
                {
                    case SerializationType.RawText:
                        return (string)_internalData;
                    case SerializationType.Json:
                        return _internalData.ToJson();
                    case SerializationType.NewtonsoftJson:
                        return _internalData.ToNewtonsoftJson();
                    case SerializationType.CamelCaseJson:
                        return _internalData.ToCamelCaseJson();
                    case SerializationType.IgnoreNullValueCamelCaseJson:
                        return _internalData.ToCamelCaseJson(ignoreNullValue: true);
                    default:
                        return _internalData.ToJson();
                }
            }
        }

        private object _internalData { get; }
        private SerializationType _serializationType { get; }

        private JsonpResult(string data)
        {
            _internalData = data;
            _serializationType = SerializationType.RawText;
        }

        private JsonpResult(object data, SerializationType serializationType)
        {
            _internalData = data;
            _serializationType = serializationType;
        }

        public static JsonpResult AsCamelCaseJson(object data, bool ignoreNullValue = false)
        {
            return ignoreNullValue
                ? new JsonpResult(data, SerializationType.IgnoreNullValueCamelCaseJson)
                : new JsonpResult(data, SerializationType.CamelCaseJson);
        }
        
        public static JsonpResult AsNewtonsoftJson(object data)
        {
            return new JsonpResult(data, SerializationType.NewtonsoftJson);
        }

        public static JsonpResult AsJson(object data)
        {
            return new JsonpResult(data, SerializationType.Json);
        }

        public static JsonpResult AsRawText(string data)
        {
            return new JsonpResult(data, SerializationType.RawText);
        }

        protected override void WriteContent(ControllerContext context, StreamWriter writer)
        {
            if (_contentType == ContentType.MsgPack)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        SerializationContext serializationContext;
                        if ((_serializationType == SerializationType.CamelCaseJson ||
                             _serializationType == SerializationType.IgnoreNullValueCamelCaseJson) &&
                            !context.RouteData.Values.ContainsKey("appcompatible"))
                        {
                            serializationContext = CamelCaseSerializationContext;
                        }
                        else
                        {
                            serializationContext = DefaultSerializationContext;
                        }

                        var serializer = MessagePackSerializer.Get(_internalData.GetType(), serializationContext);
                        serializer.Pack(stream, _internalData);
                        var bytes = stream.ToArray();
                        writer.BaseStream.Write(bytes, 0, bytes.Length);
                    }
                    return;
                }
                catch (Exception)
                {
                }

                // fall back to json
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            }

            writer.Write(Data);
        }
    }

    public enum SerializationType
    {
        RawText = 0,
        Json,
        NewtonsoftJson,
        CamelCaseJson,
        IgnoreNullValueCamelCaseJson
    }
}