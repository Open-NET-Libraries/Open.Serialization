using System.IO;
using System.Threading.Tasks;

namespace Open.Serialization
{
	public interface ISerializeAsync
	{
		ValueTask SerializeAsync<T>(Stream stream, T item);
	}

	public interface ISerializeAsync<T>
	{
		ValueTask SerializeAsync(Stream stream, T item);
	}
}
