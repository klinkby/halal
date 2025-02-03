namespace Klinkby.Halal.Tests;

[Trait("Category", "Unit")]
public class HalResourceTests
{
    [Theory]
    [InlineData("""{"_embedded":{"Dogs":[{"Id":1}]},"_links":{"find":{"href":"/dog/{id}","templated":true},"self":{"href":"/dog"}}}""")]
    public void Serialize_HalResource_should_accept_AoT_context(string expected)
    {
        Dogs dogs = new([new Dog(1)]);
        string json = JsonSerializer.Serialize(dogs, AppJsonSerializerContext.Singleton);
        Assert.Equal(expected, json);
    }
}



