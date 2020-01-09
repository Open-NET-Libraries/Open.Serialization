using System;
using Xunit;

namespace Open.Serialization.Tests
{
	public static class DefaultImplementationTests
	{
		class A : IDeserializeObject
		{
			public object Deserialize(string value, Type type)
			{
				return 1;
			}
		}

		class B : A
		{
			public T Deserialize<T>(string _)
			{
				return default;
			}
		}

		[Fact]
		public static void DefaultImplementation()
		{
			IDeserializeObject a = new A();
			Assert.Equal(1, a.Deserialize<int>("0"));
		}
	}
}
