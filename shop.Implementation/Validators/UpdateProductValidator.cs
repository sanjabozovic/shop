﻿using shop.Application.DTOs;
using shop.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Validators
{
    public class UpdateProductValidator : ProductValidator
    {
        public UpdateProductValidator(ShopContext context) : base(context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must((product, name) => !context.Products.Any(y => y.Name == name && y.Id != product.Id && y.DeletedAt == null))
                .WithMessage("Product name already exists!");
            });
        }
    }
}
