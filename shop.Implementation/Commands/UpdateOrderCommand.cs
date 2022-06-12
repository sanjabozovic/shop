using shop.Application.Commands;
using shop.Application.DTOs;
using shop.Application.Exceptions;
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
    public class UpdateOrderCommand : IUpdateOrderCommand
    {
        private readonly ShopContext _context;
        private readonly UpdateOrderValidator _validator;

        public UpdateOrderCommand(ShopContext context, UpdateOrderValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "Update order";

        public void Execute(UpdateOrderDto request)
        {
            _validator.ValidateAndThrow(request);
            var order = _context.Orders.Where(x => x.DeletedAt == null).FirstOrDefault(x => x.Id == request.Id);
            if(order == null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            order.DeliveredAt = request.DeliveredAt;
            order.DateModified = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
