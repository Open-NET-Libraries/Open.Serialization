using System;
using System.Collections.Generic;
using System.Text;

namespace Open.Serialization.Json
{
	public abstract class JsonSerializerBase : SerializerBase, IJsonSerializer
	{
	}

	public abstract class JsonSerializerBase<T> : SerializerBase<T>, IJsonSerializer<T>
	{
	}
}
