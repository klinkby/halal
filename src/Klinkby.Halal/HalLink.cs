namespace Klinkby.Halal;

/// <summary>
///     Represents a link to another resource in a HAL (Hypertext Application Language) document.
///     <seealso href="https://tools.ietf.org/html/draft-kelly-json-hal-08#section-5" />
/// </summary>
/// <param name="HRef">The URI of the linked resource.</param>
/// <param name="Templated">Indicates whether the link is templated.</param>
/// <param name="Type">The media type of the linked resource. Use <see cref="System.Net.Mime.MediaTypeNames" /> for predefined media types.</param>
/// <param name="Deprecation">A URI that provides information about the deprecation of the link.</param>
/// <param name="Name">A secondary key for selecting links which share the same relation type.</param>
public readonly record struct HalLink(
    [property: JsonPropertyName("href")] 
    Uri HRef,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [property: JsonPropertyName("templated")]
    bool? Templated = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [property: JsonPropertyName("type")]
    string? Type = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [property: JsonPropertyName("deprecation")]
    Uri? Deprecation = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [property: JsonPropertyName("name")]
    string? Name = null)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HalLink"/> struct with the specified href and optional parameters.
    /// </summary>
    /// <param name="href">The URI of the linked resource as a string.</param>
    /// <param name="templated">Indicates whether the link is templated.</param>
    /// <param name="type">The media type of the linked resource. Use <see cref="System.Net.Mime.MediaTypeNames" /> for predefined media types.</param>
    /// <param name="deprecation">A URI that provides information about the deprecation of the link.</param>
    /// <param name="name">A secondary key for selecting links which share the same relation type.</param>
    public HalLink(string href, bool? templated = null, string? type = null, Uri? deprecation = null,
        string? name = null)
        : this(new Uri(href, UriKind.RelativeOrAbsolute), templated, type, deprecation, name)
    {
    }

    /// <summary>
    ///     Creates a new <see cref="HalLink"/> instance from a <see cref="Uri"/>.
    /// </summary>
    /// <param name="href">The URI of the linked resource.</param>
    /// <returns>A new <see cref="HalLink"/> instance.</returns>
    public static HalLink FromUri(Uri href) => new(href);

    /// <summary>
    ///     Creates a new <see cref="HalLink"/> instance from a string representation of a URI.
    /// </summary>
    /// <param name="href">The URI of the linked resource as a string.</param>
    /// <returns>A new <see cref="HalLink"/> instance.</returns>
    public static HalLink FromString(string href) => new (href);

    /// <summary>
    ///     Implicitly converts a <see cref="Uri"/> to a <see cref="HalLink"/>.
    /// </summary>
    /// <param name="href">The URI of the linked resource.</param>
    /// <returns>A new <see cref="HalLink"/> instance.</returns>
    public static implicit operator HalLink(Uri href) => FromUri(href);

    /// <summary>
    ///     Implicitly converts a string representation of a URI to a <see cref="HalLink"/>.
    /// </summary>
    /// <param name="href">The URI of the linked resource as a string.</param>
    /// <returns>A new <see cref="HalLink"/> instance.</returns>
    public static implicit operator HalLink(string href) => FromString(href);
}