namespace Open.Serialization.Json;

/// <summary>
/// Basic properties needed for modifying how JSON is written.
/// </summary>
public interface IJsonSerializationOptions
{
	/// <summary>
	/// If true the serializer will format the JSON properties camelCase.
	/// </summary>
	bool? CamelCaseProperties { get; }

	/// <summary>
	/// If true the serializer will format the JSON map keys camelCase.
	/// </summary>
	bool? CamelCaseKeys { get; }

	/// <summary>
	/// If true the serializer will omit map entries with null values.
	/// </summary>
	bool? OmitNull { get; }

	/// <summary>
	/// If true the serializer will format the JSON multi-line indented.
	/// </summary>
	bool? Indent { get; }
}
