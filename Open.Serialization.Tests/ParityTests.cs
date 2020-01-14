using Open.Serialization.Json;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Open.Serialization.Tests
{
	public static class ParityTests
	{
		[Fact]
		public static async Task EnsureBasicParity()
		{
			{
				var newtonsoftFactory = new Json.Newtonsoft.JsonSerializerFactory(Json.Newtonsoft.CamelCaseJson.Minimal(true));
				var systemFactory = new Json.System.JsonSerializerFactory(Json.System.CamelCaseJson.Minimal(true));

				await CompareFactories(newtonsoftFactory, systemFactory);
			}

			{
				var newtonsoftFactory = new Json.Newtonsoft.JsonSerializerFactory();
				var systemFactory = new Json.System.JsonSerializerFactory();

				await CompareFactories(newtonsoftFactory, systemFactory);
			}

			{
				var newtonsoftFactory = new Json.Newtonsoft.JsonSerializerFactory(Json.Newtonsoft.CamelCaseJson.Minimal(true));
				var utf8JsonFactory = new Json.Utf8Json.JsonSerializerFactory(Utf8Json.Resolvers.StandardResolver.ExcludeNullCamelCase, true);

				await CompareFactories(newtonsoftFactory, utf8JsonFactory);

				var ns = newtonsoftFactory.GetObjectSerializer();
				var utf8 = utf8JsonFactory.GetObjectSerializer();

				var instance = SampleModel.Instance;
				var type = SampleModel.Instance.GetType();
				var expected = ns.Serialize(instance, type);
				var actual = utf8.Serialize(instance, type);
				if (Debugger.IsAttached) Debug.Assert(expected == actual);
				Assert.Equal(expected, actual);
			}
		}

		static async ValueTask CompareFactories(IJsonSerializerFactory expectedFactory, IJsonSerializerFactory actualFactory)
		{
			var expectedSerializer = expectedFactory.GetSerializer();
			var actualSerializer = actualFactory.GetSerializer();

			await CompareSerializer(SampleModel.Instance);
			await CompareSerializer(SampleModel.DecimalList);
			await CompareSerializer(SampleModel.DecimalLookup);
			await CompareSerializer(SampleModel.DoubleLookup);
			expectedFactory.GetSerializer().Deserialize<SampleModel>(actualSerializer.Serialize(SampleModel.Instance));

			async ValueTask CompareSerializer<T>(T instance)
			{
				var expected = expectedSerializer.Serialize(instance);
				var actual = actualSerializer.Serialize(instance);
				if (Debugger.IsAttached) Debug.Assert(expected == actual);
				Assert.Equal(expected, actual);

				using var stream = new MemoryStream();
				await actualSerializer.SerializeAsync(stream, instance);

				stream.Position = 0;
				var result = await actualSerializer.DeserializeAsync<T>(stream);
				var actualAsync = actualSerializer.Serialize(result);
				if (Debugger.IsAttached) Debug.Assert(expected == actualAsync);
				Assert.Equal(expected, actual);
			}
		}
	}
}
