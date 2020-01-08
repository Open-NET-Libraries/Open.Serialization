using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	interface ISerializeObjectAsync
	{
		ValueTask SerializeAsync(Stream target, object? item, Type type, CancellationToken cancellationToken = default);
	}
}
