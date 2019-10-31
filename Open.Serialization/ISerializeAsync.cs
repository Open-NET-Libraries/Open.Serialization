using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public interface ISerializeAsync
	{
		ValueTask SerializeAsync<T>(Stream stream, T item, CancellationToken cancellationToken = default);
	}

	public interface ISerializeAsync<in T>
	{
		ValueTask SerializeAsync(Stream stream, T item, CancellationToken cancellationToken = default);
	}
}
