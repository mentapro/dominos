using System.Text.Json.Serialization;
namespace Dominos.IntegrationTests.Models;

public class VoucherTestModel
{
    public VoucherTestModel(Guid Id, string Name, decimal Price, List<string> ProductCodes)
    {
        this.Id = Id;
        this.Name = Name;
        this.Price = Price;
        this.ProductCodes = ProductCodes;
    }
    [JsonPropertyName("id")]
    public Guid Id
    {
        get;
        init;
    }
    [JsonPropertyName("name")]
    public string Name
    {
        get;
        init;
    }
    [JsonPropertyName("price")]
    public decimal Price
    {
        get;
        init;
    }
    [JsonPropertyName("product_codes")]
    public List<string> ProductCodes
    {
        get;
        init;
    }
}