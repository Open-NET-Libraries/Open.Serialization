using FluentAssertions;
using Open.Serialization.Json.System;
using Open.Serialization.Json.System.Converters;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace Open.Serialization.Tests.System;

public static class JsonExtensionTests
{
	[Fact]
	public static void ValidateJsonExtensions()
	{
		var basic = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
		var converter = basic.GetConverter(typeof(decimal));
		Assert.NotNull(converter);

		converter = basic.GetConverter(typeof(decimal?));
		Assert.NotNull(converter); // Was null in previous versions.

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

		var serializer = basic.GetSerializer();
		var json = serializer.Serialize(SampleModel.Instance);
		var obj = serializer.Deserialize<SampleModel>(json);
		Assert.NotNull(obj);
	}

	[Fact]
	public static void ValidateDecimals()
	{
		const decimal sample = 1.234567890123456789012345m;
		var basic = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
		var serializer = basic
			.NormalizeDecimals()
			.GetSerializer();

		{
			var model = new SampleModel
			{
				DecimalValue1 = sample
			};
			var json = serializer.Serialize(model);
			Assert.Equal(sample, serializer.Deserialize<SampleModel>(json).DecimalValue1);
		}

		{
			var model = new SampleModel
			{
				NullableDecimalValue = sample
			};
			var json = serializer.Serialize(model);
			Assert.Equal(sample, serializer.Deserialize<SampleModel>(json).NullableDecimalValue);
		}
	}

	[Fact]
	public static void ValidateNulls()
	{
		Assert.Throws<ArgumentNullException>(() => RelaxedJson.Deserialize<double?>(default(string)!));
		RelaxedJson.Deserialize<double?>("null").Should().BeNull();
		Assert.Throws<JsonException>(() => RelaxedJson.Deserialize<double>("null"));
	}
}
