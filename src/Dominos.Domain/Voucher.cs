using System.Text.Json.Serialization;
namespace Dominos.Domain;

public sealed class Voucher
{
    private readonly Guid _id;
    private readonly string _name = null!;
    private readonly decimal _price;
    private readonly List<string> _productCodes = new List<string>();

    private Voucher() { }

    public Voucher(Guid id, string name, decimal price, IReadOnlyCollection<string> productCodes)
    {
        _id = id;
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _price = price;

        if (productCodes is null)
        {
            throw new ArgumentNullException(nameof(productCodes));
        }
        _productCodes.AddRange(productCodes);
    }

    [JsonPropertyName("id")]
    public Guid Id => _id;

    [JsonPropertyName("name")]
    public string Name => _name;

    [JsonPropertyName("price")]
    public decimal Price => _price;

    [JsonPropertyName("product_codes")]
    public IReadOnlyCollection<string> ProductCodes => _productCodes;
}