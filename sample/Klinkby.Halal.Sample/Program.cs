using Klinkby.Halal.Sample;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

WebApplication app = builder.Build();

IEnumerable<Breed> breeds =
[
    new(1, "Boston Terrier", "Small", "Curious"),
    new(2, "Cocker Spaniel", "Medium", "Friendly")
];

IEnumerable<Dog> dogs =
[
    new(1, "Bessie", 1),
    new(2, "Liva", 1),
    new(3, "Silva", 2)
];

RouteGroupBuilder dogResource = app.MapGroup("/dog");
dogResource.MapGet("/", () => new Dogs(dogs));
dogResource.MapGet("/{id}", (int id) =>
    dogs.FirstOrDefault(a => a.Id == id) is { } item
        ? Results.Ok(item with { Embedded = breeds.First(b => b.Id == item.BreedId).ToDogEmbedded() })
        : Results.NotFound());

RouteGroupBuilder breedResource = app.MapGroup("/breed");
breedResource.MapGet("/", () => new Breeds(breeds));
breedResource.MapGet("/{id}", (int id) =>
    breeds.FirstOrDefault(a => a.Id == id) is { } item
        ? Results.Ok(item)
        : Results.NotFound());

await app.RunAsync();