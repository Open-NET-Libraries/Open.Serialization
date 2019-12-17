using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public interface IDeserializeObjectAsync
	{
		ValueTask<object?> DeserializeAsync<T>(Stream stream, Type type, CancellationToken cancellationToken = default);
	}
}
