using FluentValidation;
using HoaccAPI.BindingModels;

namespace HoaccAPI.Validation
{
    public class EditGoalsValidator: AbstractValidator<EditGoals>
    {
        public EditGoalsValidator() {
            RuleFor(x => x.GoalsId).NotNull();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.GoalAmount).NotNull();
            RuleFor(x => x.CurrentAmount).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }

}