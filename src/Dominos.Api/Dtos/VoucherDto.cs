using System.Text.Json.Serialization;
namespace Dominos.Api.Dtos;

public sealed class VoucherDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("product_codes")]
    public IReadOnlyCollection<string> ProductCodes { get; set; } = Array.Empty<string>();
}