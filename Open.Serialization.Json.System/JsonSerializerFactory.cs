using System.Text.Json;
using System.Threading;

namespace Open.Serialization.Json.System;

/// <summary>
/// The default <see cref="IJsonSerializerFactory"/> for System.Text.Json.
/// </summary>
public class JsonSerializerFactory : IJsonSerializerFactory
{
	static readonly JsonSerializerOptions DefaultOptions = RelaxedJson.Options();
	readonly JsonSerializerOptions _options;

	/// <summary>
	/// Constructs a <see cref="JsonSerializerFactory"/>
	/// </summary>
	public JsonSerializerFactory(JsonSerializerOptions? defaultOptions)
	{
		_options = defaultOptions?.Clone() ?? DefaultOptions;
	}

	/// <inheritdoc cref="JsonSerializerFactory.JsonSerializerFactory(JsonSerializerOptions?)"/>
	public JsonSerializerFactory() : this(null)
	{
	}

	JsonSerializerInternal? _caseSensitive;
	JsonSerializerInternal? _ignoreCase;

	static JsonSerializerFactory? _default;

	/// <summary>
	/// The default <see cref="IJsonSerializerFactory"/> instance for System.Text.Json.
	/// </summary>
	public static JsonSerializerFactory Default
		=> LazyInitializer.EnsureInitialized(ref _default)!;

	JsonSerializerOptions? GetJsonSerializerSettings(IJsonSerializationOptions? options = null)
	{
		if (options is null) return null;

		var o = _options.Clone();
		if(options.OmitNull.HasValue) o.SetIgnoreNullValues(options.OmitNull.Value);
		o.WriteIndented = options.Indent ?? o.WriteIndented;
		o.DictionaryKeyPolicy = options.CamelCaseKeys == true ? JsonNamingPolicy.CamelCase : o.DictionaryKeyPolicy;
		o.PropertyNamingPolicy = options.CamelCaseProperties == true ? JsonNamingPolicy.CamelCase : o.PropertyNamingPolicy;
		return o;
	}

	internal JsonSerializerInternal GetSerializerInternal(IJsonSerializationOptions? options = null, bool caseSensitive = false)
	{
		var o = GetJsonSerializerSettings(options);
		if (o is null)
		{
#pragma warning disable CS8603 // Possible null reference return.
			return caseSensitive
				? LazyInitializer.EnsureInitialized(ref _caseSensitive,
					() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(false)))
				: LazyInitializer.EnsureInitialized(ref _ignoreCase,
					() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(true)));
#pragma warning restore CS8603 // Possible null reference return.
		}

		return new JsonSerializerInternal(o);
	}

	/// <summary>
	/// Returns an <see cref="IJsonSerializer"/>.
	/// </summary>
	public IJsonSerializer GetSerializer(IJsonSerializationOptions? options = null, bool caseSensitive = false)
		=> GetSerializerInternal(options, caseSensitive);
}
