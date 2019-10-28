using Open.Serialization.Json.System;
using Open.Serialization.Json.System.Converters;
using System.Text.Json;
using Xunit;

namespace Open.Serialization.Tests.System
{
	public static class JsonExtensionTests
	{
		[Fact]
		public static void ValidateJsonExtensions()
		{
			var basic = new JsonSerializerOptions() { IgnoreNullValues = true };
			var converter = basic.GetConverter(typeof(decimal));
			Assert.NotNull(converter);

			converter = basic.GetConverter(typeof(decimal?));
			Assert.Null(converter);

			basic.Converters.Add(JsonDecimalConverter.Instance);
			converter = basic.GetConverter(typeof(decimal));
			Assert.True(converter is JsonDecimalConverter);

			basic.RoundDecimals(2);
			converter = basic.GetConverter(typeof(decimal));
			Assert.True(converter is JsonDecimalRoundingConverter);

			basic
				.NormalizeDecimals()
				.RoundDoubles(2)
				.RoundDecimals(2);
		}
	}
}
