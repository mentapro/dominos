using System.Text.Json.Serialization;
using Dominos.Domain;
namespace Dominos.Api.Endpoints.Dtos;

public sealed class VoucherCollectionDto
{
    [JsonPropertyName("items")]
    public IReadOnlyCollection<Voucher> Items { get; set; } = Array.Empty<Voucher>();

    [JsonPropertyName("has_more_items")]
    public bool HasMoreItems { get; set; }
}