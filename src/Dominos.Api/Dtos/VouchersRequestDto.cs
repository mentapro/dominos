using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.Dtos;

public class VouchersRequestDto
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 20;

    [FromQuery(Name = "offset")]
    public int Offset { get; set; }
}