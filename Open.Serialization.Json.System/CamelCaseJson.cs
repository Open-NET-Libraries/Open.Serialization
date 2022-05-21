using System.Text.Json;

namespace Open.Serialization.Json.System;

public static class CamelCaseJson
{
	public static JsonSerializerOptions Default(bool indent = false)
	{
		var options = RelaxedJson.Options(indent);
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		return options;
	}

	public static JsonSerializerOptions Minimal(bool indent = false)
	{
		var options = Default(indent);
		options.IgnoreNullValues = true;
		return options;
	}
}
