using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Just
{
    public static class JsonExtensions
    {
        public static object DeserializeObject(this string value)
        {
            return JsonConvert.DeserializeObject(value);
        }
        public static object DeserializeObject(this string value, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject(value, settings);
        }
        public static object DeserializeObject(this string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }
        public static object DeserializeObject(this string value, Type type, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject(value, type, settings);
        }
        public static object DeserializeObject(this string value, Type type, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject(value, type, converters);
        }
        public static T DeserializeObject<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        public static T DeserializeObject<T>(this string value, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
        public static T DeserializeObject<T>(this string value, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(value, converters);
        }


        public static string SerializeObject(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static string SerializeObject(this object value, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(value, converters);
        }
        public static string SerializeObject(this object value, Formatting formatting)
        {
            return JsonConvert.SerializeObject(value, formatting);
        }
        public static string SerializeObject(this object value, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(value, settings);
        }
        public static string SerializeObject(this object value, Formatting formatting, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(value, formatting, settings);
        }
        public static string SerializeObject(this object value, Formatting formatting, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(value, formatting, converters);
        }
        public static string SerializeObject(this object value, Type type, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(value, type, settings);
        }
        public static string SerializeObject(this object value, Type type, Formatting formatting, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(value, type, formatting, settings);
        }
    }
}
