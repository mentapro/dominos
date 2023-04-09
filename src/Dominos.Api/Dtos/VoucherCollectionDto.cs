using System.Text.Json.Serialization;
namespace Dominos.Api.Dtos;

public sealed class VoucherCollectionDto
{
    [JsonPropertyName("items")]
    public IReadOnlyCollection<VoucherDto> Items { get; set; } = Array.Empty<VoucherDto>();

    [JsonPropertyName("has_more_items")]
    public bool HasMoreItems { get; set; }
}