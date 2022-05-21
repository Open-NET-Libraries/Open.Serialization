using System;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System.Converters;

public abstract class JsonValueConverterBase<T> : JsonConverter<T>
{
	public override bool CanConvert(Type objectType)
		=> objectType == typeof(T);
}
