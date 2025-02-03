namespace Klinkby.Halal.Tests.Model;

internal sealed record Dogs : HalResource<DogsEmbed>
{
    public Dogs(IEnumerable<Dog> dogs) : base(
        CreateLinks(
            "/dog", 
            new KeyValuePair<string, HalLink>(
                LinkName.Find, 
                new HalLink("/dog/{id}", templated: true))),
        new DogsEmbed(dogs))
    {
    }
}

internal sealed record DogsEmbed(IEnumerable<Dog> Dogs);

internal sealed record Dog(int Id) : HalResource;

[JsonSerializable(typeof(Dogs))]
internal sealed partial class AppJsonSerializerContext : JsonSerializerContext
{
    public static JsonSerializerOptions Singleton => new()
    {
        TypeInfoResolver = Default
    };
}


