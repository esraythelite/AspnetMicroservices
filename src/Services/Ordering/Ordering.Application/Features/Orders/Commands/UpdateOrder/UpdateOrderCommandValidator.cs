﻿using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("{Email address is required}.");

            RuleFor(p => p.TotalPrice).NotEmpty().WithMessage("{Totalprice} is required.")
                .GreaterThan(0).WithMessage("{Totalprice} should be greater than zero.");
        }
    }
}
