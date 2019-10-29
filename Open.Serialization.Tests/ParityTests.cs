using Open.Serialization.Json;
using Open.Serialization.Json.Utf8Json;
using Xunit;

namespace Open.Serialization.Tests
{
	public static class ParityTests
	{
		[Fact]
		public static void EnsureBasicParity()
		{
			{
				var newtonsoftFactory = new Json.Newtonsoft.JsonSerializerFactory(Json.Newtonsoft.CamelCaseJson.Minimal(true));
				var systemFactory = new Json.System.JsonSerializerFactory(Json.System.CamelCaseJson.Minimal(true));

				CompareFactories(newtonsoftFactory, systemFactory);
			}

			{
				var newtonsoftFactory = new Json.Newtonsoft.JsonSerializerFactory(Json.Newtonsoft.CamelCaseJson.Minimal(true));
				var systemFactory = new JsonSerializerFactory(Utf8Json.Resolvers.StandardResolver.ExcludeNullCamelCase, true);

				CompareFactories(newtonsoftFactory, systemFactory);
			}

			{
				var newtonsoftFactory = new Json.Newtonsoft.JsonSerializerFactory();
				var systemFactory = new Json.System.JsonSerializerFactory();

				CompareFactories(newtonsoftFactory, systemFactory);
			}
		}

		static void CompareFactories(IJsonSerializerFactory expectedFactory, IJsonSerializerFactory actualFactory)
		{
			var expectedSerializer = expectedFactory.GetSerializer();
			var actualSerializer = actualFactory.GetSerializer();

			Assert.Equal(expectedSerializer.Serialize(SampleModel.Instance), actualSerializer.Serialize(SampleModel.Instance));
			Assert.Equal(expectedSerializer.Serialize(SampleModel.DecimalList), actualSerializer.Serialize(SampleModel.DecimalList));
			Assert.Equal(expectedSerializer.Serialize(SampleModel.DecimalLookup), actualSerializer.Serialize(SampleModel.DecimalLookup));
			Assert.Equal(expectedSerializer.Serialize(SampleModel.DoubleLookup), actualSerializer.Serialize(SampleModel.DoubleLookup));

			expectedFactory.GetSerializer().Deserialize<SampleModel>(actualSerializer.Serialize(SampleModel.Instance));
			actualFactory.GetSerializer().Deserialize<SampleModel>(expectedSerializer.Serialize(SampleModel.Instance));
		}
	}
}
