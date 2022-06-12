using shop.Application.Commands;
using shop.Application.DTOs;
using shop.Application.Exceptions;
using shop.DataAccess;
using shop.Domain;
using shop.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Commands
{
    public class UpdateProductCommand : IUpdateProductCommand
    {
        private readonly ShopContext _context;
        private readonly UpdateProductValidator _validator;

        public UpdateProductCommand(ShopContext context, UpdateProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 10;

        public string Name => "Update product";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);
            var product = _context.Products.Where(x => x.DeletedAt == null).FirstOrDefault(x => x.Id == request.Id);

            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product));
            }

            if(request.ImagePath != null)
            {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(request.ImagePath.FileName);
                var newImage = guid + extension;

                product.ImagePath = newImage;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.BrandId = request.BrandId;
            product.ProductSizes = request.ProductSizes.Select(x =>
            {
                return new ProductSize
                {
                    SizeId = x.SizeId,
                    Quanity = x.Quantity
                };
            }).ToList();
            product.DateModified = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
