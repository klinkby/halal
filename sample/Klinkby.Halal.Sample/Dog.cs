namespace Klinkby.Halal.Sample;

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

internal sealed record Dog : HalResource<DogEmbed>
{
    public Dog(
        int id,
        string name,
        int breedId,
        Breed? breed = null
    ) : base(
        CreateLinks(
            $"/dog/{id}", 
            new KeyValuePair<string, HalLink>("breed", $"/breed/{breedId}")),
        breed?.ToDogEmbedded())
    {
        Id = id;
        Name = name;
        BreedId = breedId;
    }

    public int Id { get; }
    public string Name { get; }
    public int BreedId { get; }
}

internal sealed record DogEmbed(Breed Breed);