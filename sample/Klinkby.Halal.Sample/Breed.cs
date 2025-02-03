namespace Klinkby.Halal.Sample;

internal sealed record Breeds : HalResource<BreedsEmbed>
{
    public Breeds(IEnumerable<Breed> breeds) : base(
        CreateLinks(
            "/breed", 
            new KeyValuePair<string, HalLink>(
                LinkName.Find, 
                new HalLink("/breed/{id}", true))),
        new BreedsEmbed(breeds)
    )
    {
    }
}

internal sealed record BreedsEmbed(IEnumerable<Breed> Breeds);

internal sealed record Breed(
    int Id,
    string Name,
    string Size,
    string Temperament
) : HalResource(CreateLinks($"/breed/{Id}"))
{
    public DogEmbed ToDogEmbedded() => new(this);
}