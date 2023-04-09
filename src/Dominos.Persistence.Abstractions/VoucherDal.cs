namespace Dominos.Persistence.Abstractions
{
    public class VoucherDal
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public List<string> ProductCodes { get; set; } = null!;
    }
}