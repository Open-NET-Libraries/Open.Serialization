using System.Text.Json;

namespace Open.Serialization.Json.System;

/// <summary>
/// Provides default 'camel case' serialization configurations.
/// </summary>
public static class CamelCaseJson
{
	/// <summary>
	/// Default relaxed serializations.
	/// </summary>
	public static JsonSerializerOptions Default(bool indent = false)
	{
		var options = RelaxedJson.Options(indent);
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		return options;
	}

	/// <summary>
	/// Default relaxed serializations but also ignores null values.
	/// </summary>
	public static JsonSerializerOptions Minimal(bool indent = false)
		=> Default(indent).SetIgnoreNullValues();
}
