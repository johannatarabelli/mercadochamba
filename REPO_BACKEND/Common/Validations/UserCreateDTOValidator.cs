using backnc.Common.DTOs;
using FluentValidation;

namespace backnc.Common.Validations
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("UserName is required.");
        }
    }
}
