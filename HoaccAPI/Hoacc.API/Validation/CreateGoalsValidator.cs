using FluentValidation;
using HoaccAPI.BindingModels;

namespace HoaccAPI.Validation
{
    public class CreateGoalsValidator: AbstractValidator<CreateGoals>
    {
        public CreateGoalsValidator() {
            RuleFor(x => x.GoalsId).NotNull();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.GoalAmount).NotNull();
            RuleFor(x => x.CurrentAmount).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }

}