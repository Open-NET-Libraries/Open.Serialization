using Newtonsoft.Json;
using System;

namespace Open.Serialization.Json.Newtonsoft;

internal class JsonSerializerInternal : JsonObjectSerializerBase, IJsonSerializer
{
	readonly JsonSerializerSettings _settings;
	internal JsonSerializerInternal(JsonSerializerSettings settings)
	{
		_settings = settings;
	}

	public override T Deserialize<T>(string? value)
		=> JsonConvert.DeserializeObject<T>(value!, _settings)!;

	public override string Serialize<T>(T item)
		=> JsonConvert.SerializeObject(item, _settings);

	public override object? Deserialize(string? value, Type type)
		=> JsonConvert.DeserializeObject(value!, type);

	public override string Serialize(object? item, Type type)
		=> JsonConvert.SerializeObject(item, type, _settings);

	public new JsonSerializer<T> Cast<T>()
		=> new(Deserialize<T>, Serialize, DeserializeAsync<T>, SerializeAsync);
}
