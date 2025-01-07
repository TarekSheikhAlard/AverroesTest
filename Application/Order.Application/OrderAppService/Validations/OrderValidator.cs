using FluentValidation;
using Infrastructure.Application.Validations;
using Order.Application.OrderAppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.OrderAppService.Validations
{
    public class OrderValidator : AbstractValidator<IValidatableDto>
    {
        public OrderValidator()
        {
            RuleSet("create", () =>
            {
                RuleFor(dto => (dto as OrderCreateDto).Quantity)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("The Quantity must be greater than or equal to zero.");
            });

            RuleSet("update", () =>
            {
 
                RuleFor(dto => (dto as OrderUpdateDto).Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Quantity must be greater than or equal to zero.");

            });
        }
    }
}
