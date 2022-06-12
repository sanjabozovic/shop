using shop.Application.Commands;
using shop.Application.DTOs;
using shop.Application.Exceptions;
using shop.DataAccess;
using shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Commands
{
    public class DeleteBrandCommand : IDeleteBrandCommand
    {
        private readonly ShopContext _context;

        public DeleteBrandCommand(ShopContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Delete brand";

        public void Execute(int request)
        {
            var brand = _context.Brands.Find(request);

            if(brand == null)
            {
                throw new EntityNotFoundException(typeof(Brand));
            }

            brand.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
