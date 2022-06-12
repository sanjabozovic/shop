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
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly ShopContext _context;

        public DeleteUserCommand(ShopContext context)
        {
            _context = context;
        }

        public int Id => 13;

        public string Name => "Delete user";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            user.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
