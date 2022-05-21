using System.Text.Json;

namespace Open.Serialization.Json.System;

public static class CaseSensitiveJson
{
	public static JsonSerializerOptions Default(bool indent = false)
		=> RelaxedJson.Options(indent, true);

	public static JsonSerializerOptions Minimal(bool indent = false)
	{
		var options = Default(indent);
		options.IgnoreNullValues = true;
		return options;
	}
}
