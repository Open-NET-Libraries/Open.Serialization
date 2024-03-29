using Newtonsoft.Json;
using Open.Serialization.Json.Newtonsoft;
using System;
using Xunit;
using FluentAssertions;

namespace Open.Serialization.Tests.Newtonsoft;

public static class JsonExtensionTests
{
	[Fact]
	public static void ValidateJsonExtensions()
	{
		{
			var basic = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
			var serializer = basic
				.NormalizeDecimals()
				.RoundDoubles(2)
				.RoundDecimals(2)
				.GetSerializer();

			var json = serializer.Serialize(SampleModel.Instance);
			var obj = serializer.Deserialize<SampleModel>(json);
			Assert.NotNull(obj);
		}

		{
			var basic = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
			var serializer = basic
				.RoundDoubles(2)
				.RoundDecimals(2)
				.GetSerializer();

			var json = serializer.Serialize(SampleModel.Instance);
			var obj = serializer.Deserialize<SampleModel>(json);
			Assert.NotNull(obj);
		}

		{
			var basic = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
			var serializer = basic
				.RoundDecimals(2)
				.RoundDoubles(2)
				.GetSerializer();

			var json = serializer.Serialize(SampleModel.Instance);
			var obj = serializer.Deserialize<SampleModel>(json);
			Assert.NotNull(obj);
		}
	}

	[Fact]
	public static void ValidateDecimals()
	{
		const decimal sample = 0.234567890123456789012345m;
		{
			var serializer = CamelCaseJson
				.Default()
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

		{
			var serializer = CamelCaseJson
				.Default()
				.RoundDecimals(2)
				.GetSerializer();

			var sample2 = Math.Round(sample, 2);

			{
				var model = new SampleModel
				{
					DecimalValue1 = sample
				};
				var json = serializer.Serialize(model);
				Assert.Equal(sample2, serializer.Deserialize<SampleModel>(json).DecimalValue1);
			}

			{
				var model = new SampleModel
				{
					NullableDecimalValue = sample
				};
				var json = serializer.Serialize(model);
				Assert.Equal(sample2, serializer.Deserialize<SampleModel>(json).NullableDecimalValue);
			}
		}

		{
			var serializer = CamelCaseJson
				.Default()
				.GetSerializer();

			const int sample3 = 1;

			{
				var model = new SampleModel
				{
					DecimalValue1 = sample3
				};
				var json = serializer.Serialize(model);
				Assert.Equal(sample3, serializer.Deserialize<SampleModel>(json).DecimalValue1);
			}

			{
				var model = new SampleModel
				{
					NullableDecimalValue = sample3
				};
				var json = serializer.Serialize(model);
				Assert.Equal(sample3, serializer.Deserialize<SampleModel>(json).NullableDecimalValue);
			}
		}
	}

	[Fact]
	public static void ValidateNulls()
	{
		Assert.Throws<ArgumentNullException>(()=>RelaxedJson.Deserialize<double?>(null));
		RelaxedJson.Deserialize<double?>("null").Should().BeNull();
		Assert.Throws<NullReferenceException>(()=>RelaxedJson.Deserialize<double>("null"));
	}
}
