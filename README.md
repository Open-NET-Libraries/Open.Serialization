# Open.Serialization
DI/IoC agnostic interfaces for injecting any serialization library.

## Implementation

With the following libraries, you can build other libraries that sever their dependency from any serializer and allow you to inject whichever you want.  You can also easily integrate another serializer and still retain the DI/IoC happiness.

### Core Interfaces & Extensions

[https://www.nuget.org/packages/Open.Serialization](https://www.nuget.org/packages/Open.Serialization/)  
[![NuGet](http://img.shields.io/nuget/v/Open.Serialization.svg)](https://www.nuget.org/packages/Open.Serialization/) Core package for serializing anything.

[https://www.nuget.org/packages/Open.Serialization.Json](https://www.nuget.org/packages/Open.Serialization.Json/)  
[![NuGet](http://img.shields.io/nuget/v/Open.Serialization.Json.svg)](https://www.nuget.org/packages/Open.Serialization.Json/) Core package specific to JSON.

### Library/Vendor Specific Implementations

```cs
services.AddJsonSerializer();
```

The following libs contain support for `Microsoft.Extensions.DependencyInjection`.  
Import any of these and you can use the above extension to inject default serializers.

[https://www.nuget.org/packages/Open.Serialization.Json.Newtonsoft](https://www.nuget.org/packages/Open.Serialization.Json.Newtonsoft/)  
[![NuGet](http://img.shields.io/nuget/v/Open.Serialization.Json.Newtonsoft.svg)](https://www.nuget.org/packages/Open.Serialization.Json.Newtonsoft/) Extensions and DI for **Newtonsoft.Json**.

[https://www.nuget.org/packages/Open.Serialization.Json.System](https://www.nuget.org/packages/Open.Serialization.Json.System/)  
[![NuGet](http://img.shields.io/nuget/v/Open.Serialization.Json.System.svg)](https://www.nuget.org/packages/Open.Serialization.Json.System/) Extensions and DI for **System.Text.Json**.  *Note: There is no `IJsonObjectSerializer` option for `System.Text.Json`.*

[https://www.nuget.org/packages/Open.Serialization.Json.Utf8Json](https://www.nuget.org/packages/Open.Serialization.Json.Utf8Json/)  
[![NuGet](http://img.shields.io/nuget/v/Open.Serialization.Json.Utf8Json.svg)](https://www.nuget.org/packages/Open.Serialization.Json.Utf8Json/) Extensions and DI for **Utf8Json**.

## Interface & Methods Exposed

```cs
T IDeserialize.Deserialize<T>(string value);
T IDeserialize<T>.Deserialize(string value);
object IDeserializeObject.Deserialize(string value, Type type);

ValueTask<T> IDeserializeAsync.DeserializeAsync<T>(Stream source, CancellationToken cancellationToken = default);
ValueTask<T> IDeserializeAsync<T>.DeserializeAsync(Stream source, CancellationToken cancellationToken = default);
ValueTask<object> IDeserializeObjectAsync.DeserializeAsync(Stream source, Type type, CancellationToken cancellationToken = default);

string ISerialize.Serialize<T>(T item);
string ISerialize<T>.Serialize(T item);
string ISerializeObject.Serialize(object item, Type type);

ValueTask ISerializeAsync.SerializeAsync<T>(Stream target, T item, CancellationToken cancellationToken = default);
ValueTask ISerializeAsync<T>.SerializeAsync(Stream target, T item, CancellationToken cancellationToken = default);
ValueTask ISerializeObjectAsync.SerializeAsync(Stream target, object item, Type type, CancellationToken cancellationToken = default);
```
