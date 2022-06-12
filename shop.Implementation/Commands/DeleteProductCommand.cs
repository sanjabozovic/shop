﻿using shop.Application.Commands;
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
    public class DeleteProductCommand : IDeleteProductCommand
    {
        private readonly ShopContext _context;

        public DeleteProductCommand(ShopContext context)
        {
            _context = context;
        }
        public int Id => 11;

        public string Name => "Delete product";

        public void Execute(int request)
        {
            var product = _context.Products.Find(request);

            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product));
            }

            product.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
