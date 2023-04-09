using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.Dtos;

public class VouchersAutocompleteRequestDto
{
    [FromQuery(Name = "name_search")]
    public string NameSearch { get; set; } = null!;

    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 20;

    [FromQuery(Name = "offset")]
    public int Offset { get; set; }
}