using shop.Application.Commands;
using shop.Application.Exceptions;
using shop.Application.Interfaces;
using shop.DataAccess;
using shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Commands
{
    public class DeleteOrderCommand : IDeleteOrderCommand
    {
        private readonly ShopContext _context;
        private readonly IApplicationActor _actor;

        public DeleteOrderCommand(ShopContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 16;

        public string Name => "Delete order";

        public void Execute(int request)
        {
            var order = _context.Orders.Find(request);

            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            order.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
