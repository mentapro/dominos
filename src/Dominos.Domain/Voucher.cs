namespace Dominos.Domain;

public sealed record VoucherId(Guid Id);

public sealed class Voucher
{
    public Voucher(VoucherId id, string name, decimal price, List<string> productCodes)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
        ProductCodes = productCodes ?? throw new ArgumentNullException(nameof(productCodes));
    }

    public VoucherId Id { get; }

    public string Name { get; }

    public decimal Price { get; }

    public List<string> ProductCodes { get; }

    public static Voucher New(VoucherId id, string name, decimal price, List<string> productCodes)
    {
        var voucher = new Voucher(id, name, price, productCodes);
        return voucher;
    }
}