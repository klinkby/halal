# Klinkby.Halal

[![Workflow](https://github.com/klinkby/halal/actions/workflows/main.yml/badge.svg)](https://github.com/klinkby/toolkitt/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/klinkby/halal/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/klinkby/toolkitt/actions/workflows/github-code-scanning/codeql)
[![NuGet](https://img.shields.io/nuget/v/Klinkby.Halal.svg)](https://www.nuget.org/packages/Klinkby.Halal/)
[![GitHub](https://img.shields.io/github/license/Klinkby/Halal.svg)](https://github.com/klinkby/halal/blob/main/LICENSE)

The `Klinkby.Halal` project provides a set of immutable classes that serve as base for creating and managing resources 
and links in HAL (Hypertext Application Language) documents. It is designed to facilitate the implementation of 
hypermedia-driven RESTful APIs in .NET.

## Features

- **HAL Resource Representation**: Define and manage resources using the `HalResource` and `HalResource<TEmbedded>`
  records.
- **Link Management**: Easily add and manage links to related resources using extension methods.
- **Constants for Link Relations**: Use predefined constants for common link relation types.
- **JSON Serialization**: AoT-friendly `System.Text.Json` serialization.

## Prerequisites

- [.NET Standard 2.1](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-1)

## Installation

To install `Klinkby.Halal`, add the following package reference to your project file:

```xml
<PackageReference Include="Klinkby.Halal" Version="1.0.0-alpha" />
```

## Usage

The solution provide a sample using the Klinkby.Halal in a AoT Web API project.

A complete resource could be defined as following sample:

```csharp
public record Dogs : HalResource<DogsEmbed>
{
    public Dogs(IEnumerable<Dog> dogs) : base(
        HalLinks.Self(
            "/dog", 
            new KeyValuePair<string, HalLink>(
                LinkName.Find, 
                new HalLink("/dog/{id}", templated: true))),
        new DogsEmbed(dogs))
    {
    }
}

public record DogsEmbed(IEnumerable<Dog> Dogs);

public record Dog : HalResource<DogEmbed>
{
    public Dog(
        int id,
        string name,
        int breedId,
        Breed? breed = null
    ) : base(
        HalLinks.Self(
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

public record DogEmbed(Breed Breed);
```

To provide a collection response like:

```json
{
  "_embedded": {
    "dogs": [
      {
        "id": 1,
        "name": "Bessie",
        "breedId": 1,
        "_links": {
          "breed": {
            "href": "/breed/1"
          },
          "self": {
            "href": "/dog/1"
          }
        }
      },
      {
        "id": 2,
        "name": "Liva",
        "breedId": 1,
        "_links": {
          "breed": {
            "href": "/breed/1"
          },
          "self": {
            "href": "/dog/2"
          }
        }
      }
    ]
  },
  "_links": {
    "find": {
      "href": "/dog/{id}",
      "templated": true
    },
    "self": {
      "href": "/dog"
    }
  }
}
```

And an item reponse like:

```json
{
  "id": 1,
  "name": "Bessie",
  "breedId": 1,
  "_embedded": {
    "breed": {
      "id": 1,
      "name": "Boston Terrier",
      "size": "Small",
      "temperament": "Curious",
      "_links": {
        "self": {
          "href": "/breed/1"
        }
      }
    }
  },
  "_links": {
    "breed": {
      "href": "/breed/1"
    },
    "self": {
      "href": "/dog/1"
    }
  }
}
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.