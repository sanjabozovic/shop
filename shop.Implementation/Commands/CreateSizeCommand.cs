using shop.Application.Commands;
using shop.Application.DTOs;
using shop.DataAccess;
using shop.Domain;
using shop.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Commands
{
    public class CreateSizeCommand : ICreateSizeCommand
    {
        private readonly ShopContext _context;
        private readonly CreateSizeValidator _validator;

        public CreateSizeCommand(ShopContext context, CreateSizeValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Insert size";

        public void Execute(SizeDto request)
        {
            _validator.ValidateAndThrow(request);

            var size = new Size
            {
                SizeValue = request.SizeValue
            };

            _context.Sizes.Add(size);
            _context.SaveChanges();
        }
    }
}
