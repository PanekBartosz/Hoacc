using FluentValidation;
using HoaccAPI.BindingModels;

namespace HoaccAPI.Validation
{
    public class CreateUserValidator: AbstractValidator<CreateUser>
    {
        public CreateUserValidator() {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Password).NotNull();
        }
    }

}