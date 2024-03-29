﻿using Open.Serialization.Extensions;
using System;
using Xunit;

namespace Open.Serialization.Tests;

public static class DefaultImplementationTests
{
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1163:Unused parameter.")]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter")]
	class A : IDeserializeObject
	{
		public object Deserialize(string value, Type type) => 1;

		public object Deserialize(ReadOnlySpan<char> value, Type type) => 1;
	}

	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static")]
	class B : A
	{
		public T Deserialize<T>(string _) => default;
	}

	class C : B, IDeserializeObject
	{
	}

	[Fact]
	public static void DefaultImplementation()
	{
		IDeserializeObject a1 = new A();
		Assert.Equal(1, a1.Deserialize<int>("0"));

		var b1 = new B();
		Assert.Equal(0, b1.Deserialize<int>("0"));

		IDeserializeObject b2 = new B();
		Assert.Equal(1, b2.Deserialize<int>("0"));

		var c1 = new C();
		Assert.Equal(0, c1.Deserialize<int>("0"));
	}
}
