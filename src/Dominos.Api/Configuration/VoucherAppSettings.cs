using System.ComponentModel.DataAnnotations;
using Dominos.Persistence.Abstractions;

namespace Dominos.Api.Configuration;

public class VoucherAppSettings
{
    [Required]
    public DbConnectionOptions PostgresOptions { get; set; } = null!;
}