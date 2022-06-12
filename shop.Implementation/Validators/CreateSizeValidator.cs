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
    public class CreateSizeValidator : AbstractValidator<SizeDto>
    {
        public CreateSizeValidator(ShopContext context)
        {
            RuleFor(x => x.SizeValue).NotEmpty().WithMessage("Size value is required.").DependentRules(() =>
            {
                RuleFor(x => x.SizeValue).Must(x => x <= 15).WithMessage("Max size value is 15.").DependentRules(() => 
                {
                    RuleFor(x => x.SizeValue).Must(x => !context.Sizes.Any(y => y.SizeValue == x)).WithMessage("Size already exists.");
                });
            });
        }
    }
}
