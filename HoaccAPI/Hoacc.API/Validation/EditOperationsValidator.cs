﻿using FluentValidation;
using HoaccAPI.BindingModels;

namespace HoaccAPI.Validation
{
    public class EditOperationsValidator: AbstractValidator<EditOperations>
    {
        public EditOperationsValidator() {
            RuleFor(x => x.OperationId).NotNull();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.Category).NotNull();
            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }

}