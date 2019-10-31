using Open.Serialization.Json.Newtonsoft;
using Newtonsoft.Json;
using Xunit;

namespace Open.Serialization.Tests.Newtonsoft
{
	public static class JsonExtensionTests
	{
		[Fact]
		public static void ValidateJsonExtensions()
		{
			var basic = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

			basic
				.NormalizeDecimals()
				.RoundDoubles(2)
				.RoundDecimals(2);

			var serializer = basic.GetSerializer();
			var json = serializer.Serialize(SampleModel.Instance);
			var obj = serializer.Deserialize<SampleModel>(json);
			Assert.NotNull(obj);
		}
	}
}
