using FluentValidation;
using WebApplication1.Application.ErrorMessages;
using WebApplication1.Application.User.Create;

namespace WebApplication1.Web.Validators
{
    public class UserValidator : AbstractValidator<RequestCreate>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage(UserErrorMessages.INVALID_NAME);
            RuleFor(p => p.Phone).NotNull().WithMessage(UserErrorMessages.INVALID_PHONE);
            RuleFor(p => p.Address).NotNull().WithMessage(UserErrorMessages.INVALID_ADDRESS);
            RuleFor(p => p.Email).NotNull().WithMessage(UserErrorMessages.INVALID_EMAIL);
        }
    }
}
