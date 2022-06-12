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
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        public CreateOrderValidator(ShopContext context)
        {
            RuleFor(x => x.OrderLines).NotEmpty().WithMessage("Order lines are required.").DependentRules(() =>
            {
                RuleFor(x => x.OrderLines).Must(orderLines =>
                    orderLines.Select(o => o.ProductId + o.SizeId).Distinct().Count() == orderLines.Count()
                ).WithMessage("There are duplicate order lines.").DependentRules(() =>
                {
                    RuleForEach(x => x.OrderLines).SetValidator(new OrderLineValidator(context));
                });
            });
        }
    }
}
