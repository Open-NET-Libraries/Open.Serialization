using System;
using System.Collections.Generic;
using System.Text;

namespace Open.Serialization.Json
{
	public interface IJsonSerializerFactory
	{
		IJsonSerializer GetSerializer(IJsonSerializationOptions options = null, bool caseSensitive = false);
	}
}
