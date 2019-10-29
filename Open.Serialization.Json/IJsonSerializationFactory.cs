﻿namespace Open.Serialization.Json
{
	public interface IJsonSerializationFactory
	{
		IJsonDeserialize GetDeserializer(bool caseSensitive = false);
		IJsonDeserializeAsync GetAsyncDeserializer(bool caseSensitive = false);

		IJsonSerialize GetSerializer(IJsonSerializationOptions options = null);
		IJsonSerializeAsync GetAsyncSerializer(IJsonSerializationOptions options = null);
	}
}
