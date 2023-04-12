using Dominos.Api.Endpoints.Dtos;
using FluentValidation;
namespace Dominos.Api.Endpoints;

public class VouchersRequestDtoValidator : AbstractValidator<VouchersRequestDto>
{
    public VouchersRequestDtoValidator()
    {
        RuleFor(x => x.Limit).GreaterThan(0);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
    }
}

public class VouchersAutocompleteRequestDtoValidator : AbstractValidator<VouchersAutocompleteRequestDto>
{
    public VouchersAutocompleteRequestDtoValidator()
    {
        RuleFor(x => x.NameSearch).NotEmpty();
        RuleFor(x => x.Limit).GreaterThan(0);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
    }
}