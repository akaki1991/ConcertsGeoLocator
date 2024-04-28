using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ConcertsGeoLocator.Shared;

public class Serializer
{
    public static T As<T>(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return default(T);
        }

        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            DateFormatString = SystemSettings.ShortDatePattern
        };
        settings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = SystemSettings.LongDatePattern });
        return JsonConvert.DeserializeObject<T>(json, settings);
    }

    public static string Serialize(object value, bool typeNameHandling = true, bool camelCase = false)
    {
        var settings = new JsonSerializerSettings
        {
            DateFormatString = SystemSettings.ShortDatePattern,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };

        if (typeNameHandling)
        {
            settings.TypeNameHandling = TypeNameHandling.Objects;
        }

        if (camelCase)
        {
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        settings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = SystemSettings.LongDatePattern });
        var json = JsonConvert.SerializeObject(value, settings);

        return json;
    }

    public T GetPropertyValue<T>(string json, string propertyPath)
    {
        var jo = JObject.Parse(json);
        var token = jo.SelectToken(propertyPath);
        return token.ToObject<T>();
    }

    public T DeepCopy<T>(T obj, bool typeNameHandling = true)
    {
        var json = Serialize(obj, typeNameHandling);
        return As<T>(json);
    }
}

public static class SystemSettings
{
    public const string ShortDatePattern = "dd-MM-yyyy";
    public const string LongDatePattern = "dd-MM-yyyy HH:mm:ss.FFFFFFFK";
    public const string DateShortTimePattern = "dd-MM-yyyy HH:mm";
    public const string DateTimeOffsetShortTimePattern = "dd-MM-yyyy HH:mm:ss";
    public const string ShortTimePattern = "HH:mm";
    public const string LongTimePattern = "HH:mm:ss";
    public const string NumberDecimalSeparator = ".";
    public const string NumberGroupSeparator = " ";
    public const string DatePatternForMomentjs = "yyyy-MM-dd HH:mm:ss";
    public const string MomentToDateTimeOffset = "ddd MMM dd yyyy HH:mm:ss";
    public static string FullDateTimeOffsetPattern = "dd-MM-yyyyTHH:mm:ss.FFFFFFFK";
}
