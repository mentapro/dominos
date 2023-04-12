using System.Text.Json.Serialization;
namespace Dominos.IntegrationTests.Models;

public sealed class VoucherCollectionTestDto
{
    [JsonPropertyName("items")]
    public IReadOnlyCollection<VoucherTestModel> Items { get; set; } = Array.Empty<VoucherTestModel>();

    [JsonPropertyName("has_more_items")]
    public bool HasMoreItems { get; set; }
}