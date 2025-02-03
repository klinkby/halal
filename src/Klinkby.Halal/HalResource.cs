namespace Klinkby.Halal;

/// <summary>
///     Represents a resource in a HAL (Hypertext Application Language) document.
///     <seealso href="https://tools.ietf.org/html/draft-kelly-json-hal-08#section-4" />
/// </summary>
/// <param name="Links">A dictionary of links to related resources.</param>
public record HalResource(
    [property: JsonPropertyName("_links")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    IReadOnlyDictionary<string, HalLink>? Links = null)
{
    /// <summary>
    ///     Creates a dictionary of HAL links with a self link and additional links.
    /// </summary>
    /// <param name="self">The self link representing the current resource.</param>
    /// <param name="additionalLinks">Additional links to be included in the dictionary.</param>
    /// <returns>A read-only dictionary of HAL links.</returns>
    public static IReadOnlyDictionary<string, HalLink> CreateLinks(HalLink self,
        params ReadOnlySpan<KeyValuePair<string, HalLink>> additionalLinks)
    {
        int capacity = additionalLinks.Length + 1;
        LinkMap links = new(capacity)
        {
            [LinkName.Self] = self
        };

        int count = additionalLinks.Length;
        for (int i = 0; i < count; i++)
        {
            (string key, HalLink value) = additionalLinks[i];
            links[key] = value;
        }

        return links;
    }
}

/// <summary>
///     Represents a resource in a HAL (Hypertext Application Language) document with embedded resources.
/// </summary>
/// <typeparam name="TEmbedded">The type of the embedded resource.</typeparam>
/// <param name="Links">A dictionary of links to related resources.</param>
/// <param name="Embedded">The embedded resource for optimization.</param>
public record HalResource<TEmbedded>(
    IReadOnlyDictionary<string, HalLink>? Links,
    [property: JsonPropertyName("_embedded")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    TEmbedded? Embedded = default)
    : HalResource(Links);