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
    public class UpdateBrandValidator : AbstractValidator<BrandDto>
    {
        public UpdateBrandValidator(ShopContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must((brand, name) => !context.Brands.Any(x => x.Name == name && x.Id != brand.Id && x.DeletedAt == null))
                .WithMessage("Brand name already exists.");
            });
        }
    }
}
