using System.Net.Mime;

namespace Klinkby.Halal.Tests;

[Trait("Category", "Unit")]
public class HalLinkTests
{
    [Theory]
    [InlineData(
        """{"href":"/dog/{id}/image","templated":true,"type":"image/jpeg","deprecation":"/deprecation-note","name":"imageId"}""")]
    public void Serialize_HalLink_should_match_properties(string expected)
    {
        HalLink link = new("/dog/{id}/image", true, MediaTypeNames.Image.Jpeg,
            new Uri("/deprecation-note", UriKind.Relative), "imageId");
        string json = JsonSerializer.Serialize(link, AppJsonSerializerContext.Singleton);
        Assert.Equal(expected, json);
    }
    
    [Fact]
    public void HalLink_FromUri_should_Convert()
    {
        Uri expected = new("/dog", UriKind.Relative);
        HalLink link = HalLink.FromUri(expected);
        Assert.Equal(expected, link.HRef);
    }

    [Fact]
    public void HalLink_implicit_should_Convert()
    {
        Uri expected = new("/dog", UriKind.Relative);
        HalLink link = expected;
        Assert.Equal(expected, link.HRef);
    }
}