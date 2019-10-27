using System.Threading.Tasks;

namespace Open.Serialization
{
	public interface ISerializeAsync
	{
		ValueTask<string> SerializeAsync<T>(T item);
	}

	public interface ISerializeAsync<T>
	{
		ValueTask<string> SerializeAsync(T item);
	}
}
