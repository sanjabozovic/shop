using shop.Application.Commands;
using shop.Application.DTOs;
using shop.Application.Exceptions;
using shop.Application.Interfaces;
using shop.DataAccess;
using shop.Domain;
using shop.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Commands
{
    public class CreateOrderCommand : ICreateOrderCommand
    {
        private readonly ShopContext _context;
        private readonly IApplicationActor _actor;
        private readonly CreateOrderValidator _validator;

        public CreateOrderCommand(ShopContext context, IApplicationActor actor, CreateOrderValidator validator)
        {
            _context = context;
            _actor = actor;
            _validator = validator;
        }

        public int Id => 14;

        public string Name => "Create order";

        public void Execute(OrderDto request)
        {
            _validator.ValidateAndThrow(request);

            var order = new Order
            {
                PlacedAt = DateTime.UtcNow,
                UserId = _actor.Id,
                OrderLines = request.OrderLines.Select(x =>
                {
                    var productSize = _context.ProductSizes.Where(y => y.ProductId == x.ProductId)
                    .Where(y => y.SizeId == x.SizeId)
                    .First();

                    if(productSize == null)
                    {
                        throw new EntityNotFoundException(typeof(ProductSize));
                    }

                    if(productSize.Quanity < x.Quantity)
                    {
                        throw new Exception("Not enough in stock!");
                    }

                    var product = _context.Products.Find(x.ProductId);
                    productSize.Quanity = productSize.Quanity - x.Quantity;
                    return new OrderLine
                    {
                        ProductName = product.Name,
                        Price = product.Price,
                        ProductSizeId = productSize.Id,
                        Quantity = x.Quantity
                    };
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
