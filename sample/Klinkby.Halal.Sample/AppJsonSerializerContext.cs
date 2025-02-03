using System.Text.Json.Serialization;

namespace Klinkby.Halal.Sample;

[JsonSerializable(typeof(Breeds))]
[JsonSerializable(typeof(Dogs))]
internal sealed partial class AppJsonSerializerContext : JsonSerializerContext;