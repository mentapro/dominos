using System.ComponentModel.DataAnnotations;
using Dominos.Persistence.Abstractions;
namespace Dominos.Api;

public class VoucherAppSettings
{
    [Required]
    public DbConnectionOptions PostgresOptions { get; set; } = null!;
}