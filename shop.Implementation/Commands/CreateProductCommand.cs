using AutoMapper;
using shop.Application.Commands;
using shop.Application.DTOs;
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
    public class CreateProductCommand : ICreateProductCommand
    {
        private readonly ShopContext _context;
        private readonly CreateProductValidator _validator;

        public CreateProductCommand(ShopContext context, CreateProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 9;

        public string Name => "Insert product";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);

            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(request.ImagePath.FileName);
            var newImage = guid + extension;

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                ImagePath = newImage,
                Price = request.Price,
                BrandId = request.BrandId,
                ProductSizes = request.ProductSizes.Select(x =>
                {
                    return new ProductSize
                    {
                        SizeId = x.SizeId,
                        Quanity = x.Quantity
                    };
                }).ToList()
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
