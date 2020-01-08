using System;

namespace Open.Serialization
{
	interface ISerializeObject
	{
		string? Serialize(object? item, Type type);
	}
}
