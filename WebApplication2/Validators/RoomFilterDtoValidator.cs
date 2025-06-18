namespace WebApplication2.Validators;

using FluentValidation;
using WebApplication2.DTO;

    public class RoomFilterDtoValidator : AbstractValidator<RoomFilterDto>
    {
        public RoomFilterDtoValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
            RuleFor(x => x.SortDirection).Must(x => x == "asc" || x == "desc").WithMessage("SortDirection must be 'asc' or 'desc'");
        }
    }
