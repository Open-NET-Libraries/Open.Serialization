using System.Text.Json;

namespace Open.Serialization.Json.System;

/// <summary>
/// Provides default 'case sensitive' serialization configurations.
/// </summary>
public static class CaseSensitiveJson
{
	/// <summary>
	/// Default relaxed serializations.
	/// </summary>
	public static JsonSerializerOptions Default(bool indent = false)
		=> RelaxedJson.Options(indent, true);

	/// <summary>
	/// Default relaxed serializations but also ignores null values.
	/// </summary>
	public static JsonSerializerOptions Minimal(bool indent = false)
		=> Default(indent).SetIgnoreNullValues();
}
