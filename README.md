# Open.Serialization
DI/IoC agnostic interfaces for injecting any serialization library.

## Implementation

With the following libraries, you can build other libraries that sever their dependency from any serializer and allow you to inject whichever you want.  You can also easily integrate another serializer and still retain the DI/IoC happiness.

### Core Interfaces & Extensions

[https://www.nuget.org/packages/Open.Serialization/](https://www.nuget.org/packages/Open.Serialization/) Core package for serializing anything.

[https://www.nuget.org/packages/Open.Serialization.Json/](https://www.nuget.org/packages/Open.Serialization.Json/) Core package specific to JSON.

### Library/Vendor Specific Implementations

```cs
services.AddJsonSerializer();
```

The following libs contain support for `Microsoft.Extensions.DependencyInjection`.  Import any of these and you can use the above extension to inject default serializers.

[https://www.nuget.org/packages/Open.Serialization.Json.Newtonsoft/](https://www.nuget.org/packages/Open.Serialization.Json.Newtonsoft/) Extensions and DI for **Newtonsoft.Json**.

[https://www.nuget.org/packages/Open.Serialization.Json.System/](https://www.nuget.org/packages/Open.Serialization.Json.System/) Extensions and DI for **System.Text.Json**.  Note: There is no option for `IJsonObjectSerializer` for `System.Text.Json`.

[https://www.nuget.org/packages/Open.Serialization.Json.Utf8Json](https://www.nuget.org/packages/Open.Serialization.Json.Utf8Json) Extensions and DI for **Utf8Json**.



