using shop.Application.DTOs;
using shop.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Validators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator(ShopContext context)
        {
            RuleFor(x => x.DeliveredAt).NotEmpty().WithMessage("Delivered date is required.").DependentRules(() => 
            {
                RuleFor(x => x.DeliveredAt).Must(x => x <= DateTime.UtcNow);
            }
            );
        }
    }
}
