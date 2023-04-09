using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.Dtos;

public class VouchersRequestDto
{
    //[FromQuery(Name = "name")]
    public string? Name { get; set; }

    //FromQuery(Name = "limit")]
    public int Limit { get; set; } = 20;

    //[FromQuery(Name = "offset")]
    public int Offset { get; set; }

    public static ValueTask<VouchersRequestDto> BindAsync(HttpContext context)
    {
        var name = context.Request.Query["name"].ToString();
        int.TryParse(context.Request.Query["limit"], out var limit);
        int.TryParse(context.Request.Query["offset"], out var offset);
        var result = new VouchersRequestDto
        {
            Name = name,
            Limit = limit,
            Offset = offset,
        };

        return ValueTask.FromResult(result);
    }
}