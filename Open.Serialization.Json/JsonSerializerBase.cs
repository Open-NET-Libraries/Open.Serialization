namespace Open.Serialization.Json
{
	/// <inheritdoc />
	public abstract class JsonSerializerBase : SerializerBase, IJsonSerializer, IJsonAsyncSerializer
	{
		/// <inheritdoc cref="SerializerBase.Cast{T}" />
		public new JsonSerializer<T> Cast<T>()
			=> new JsonSerializer<T>(Deserialize<T>, Serialize, DeserializeAsync<T>, SerializeAsync);
	}

	/// <inheritdoc />
	public abstract class JsonSerializerBase<T> : SerializerBase<T>, IJsonSerializer<T>, IJsonAsyncSerializer<T>
	{
	}
}
