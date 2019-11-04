using System;

namespace Open.Serialization
{
	public interface IDeserializeObject
	{
		object Deserialize(string value, Type type);
	}
}
