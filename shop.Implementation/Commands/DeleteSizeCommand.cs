using shop.Application.Commands;
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
    public class DeleteSizeCommand : IDeleteSizeCommand
    {
        private readonly ShopContext _context;

        public DeleteSizeCommand(ShopContext context)
        {
            _context = context;
        }
        public int Id => 8;

        public string Name => "Delete size";

        public void Execute(int request)
        {
            var size = _context.Sizes.Find(request);

            if (size == null)
            {
                throw new EntityNotFoundException(typeof(Size));
            }

            size.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
