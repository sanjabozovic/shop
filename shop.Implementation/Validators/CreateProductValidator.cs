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
    public class CreateProductValidator : ProductValidator
    {
        public CreateProductValidator(ShopContext context) : base(context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must(x => !context.Products.Any(y => y.Name == x && y.DeletedAt == null))
                .WithMessage("Product name already exists!");
            });
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Image path is required.");
        }
    }
}
