using System;
using System.Text.Json.Serialization;

namespace Open.Serialization.Json.System.Converters;

/// <summary>Base class for other json value converters.</summary>
/// <inheritdoc />
public abstract class JsonValueConverterBase<T> : JsonConverter<T>
{
	/// <inheritdoc />
	public override bool CanConvert(Type objectType)
		=> objectType == typeof(T);
}
