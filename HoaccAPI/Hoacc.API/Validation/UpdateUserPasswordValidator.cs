using FluentValidation;
using HoaccAPI.BindingModels;

namespace HoaccAPI.Validation;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPassword>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(x => x.Password).NotNull();
    }
}