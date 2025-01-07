using FluentValidation;
using Infrastructure.Application.Validations;
using Inventory.Application.InventoryAppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.InventoryAppService.Validations
{
    public class InventoryValidator : AbstractValidator<IValidatableDto>
    {
        public InventoryValidator()
        {
            RuleSet("create", () =>
            {
                RuleFor(dto => (dto as InventoryCreateDto).QuantityInStock)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("The Quantity must be greater than or equal to zero.");
            });

            RuleSet("update", () =>
            {

                RuleFor(dto => (dto as InventoryUpdateDto).QuantityInStock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Quantity must be greater than or equal to zero.");

            });
        }
    }
}
