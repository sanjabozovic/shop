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
    public class CreateBrandValidator : AbstractValidator<BrandDto>
    {
        public CreateBrandValidator(ShopContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must(x => !context.Brands.Any(y => y.Name == x && y.DeletedAt == null))
                .WithMessage("Brand name already exists.");
            });
        }
    }
}
