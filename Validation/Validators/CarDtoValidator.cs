using Domain.Dto;
using Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Api.Validators
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(x => x.Owner).NotEmpty();
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Weight).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Color).IsInEnum();
            RuleFor(x => x.PlateNumber).Matches(new Regex(@"^[А-Яа-я]\d{3}[А-Яа-я]{2}\d{2,3}$"));
        }
    }
}
