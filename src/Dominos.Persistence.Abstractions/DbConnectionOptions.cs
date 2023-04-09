using System.ComponentModel.DataAnnotations;
namespace Dominos.Persistence.Abstractions;

public class DbConnectionOptions
{
    [Required(AllowEmptyStrings = false)]
    public string ConnectionString { get; set; } = default!;
}