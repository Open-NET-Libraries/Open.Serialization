﻿using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;

namespace Open.Serialization.Json.Newtonsoft.Converters;

public class JsonNullableDecimalConverter : JsonValueConverterBase<decimal?>
{
	protected JsonNullableDecimalConverter()
	{
		// Prevent unnecessary replication.
	}

	public static readonly JsonNullableDecimalConverter Instance
		= new();

	public override decimal? ReadJson(JsonReader reader, Type objectType, decimal? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader is null) throw new ArgumentNullException(nameof(reader));
		Contract.EndContractBlock();

		return reader.TokenType switch
		{
			JsonToken.Null => default,
			JsonToken.Undefined => default,
			_ => ConvertToDecimal(reader.Value),
		};
	}

	public override void WriteJson(JsonWriter writer, decimal? value, JsonSerializer serializer)
	{
		if (writer is null) throw new ArgumentNullException(nameof(writer));
		Contract.EndContractBlock();

		writer.WriteRawValue(JsonDecimalConverter.Normalize(value));
	}
}
