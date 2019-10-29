using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public interface IDeserializeAsync
	{
		ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default);
	}

	public interface IDeserializeAsync<T>
	{
		ValueTask<T> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default);
	}
}
