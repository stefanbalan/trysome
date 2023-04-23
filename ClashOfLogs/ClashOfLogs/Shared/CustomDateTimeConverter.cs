using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public class CustomDateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        //"endTime": "20210522T083133.000Z",
        DateTime.ParseExact(reader.GetString()!, "yyyyMMddTHHmmss.000Z", CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("yyyyMMddTHHmmss.000Z", CultureInfo.InvariantCulture));
}