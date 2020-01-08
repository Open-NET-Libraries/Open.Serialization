using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public interface IDeserializeObjectAsync
	{
		ValueTask<object?> DeserializeAsync(Stream source, Type type, CancellationToken cancellationToken = default);
	}
}
