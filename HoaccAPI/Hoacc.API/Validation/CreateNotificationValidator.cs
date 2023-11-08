using FluentValidation;
using HoaccAPI.BindingModels;

namespace HoaccAPI.Validation
{
    public class CreateNotificationValidator: AbstractValidator<CreateNotification>
    {
        public CreateNotificationValidator() {
            RuleFor(x => x.NotificationId).NotNull();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Date).NotNull();
            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }

}