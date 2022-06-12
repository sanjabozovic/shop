using AutoMapper;
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
    public class UpdateBrandCommand : IUpdateBrandCommand
    {
        private readonly ShopContext _context;
        private readonly UpdateBrandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateBrandCommand(ShopContext context, UpdateBrandValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }
        public int Id => 5;

        public string Name => "Update brand";

        public void Execute(BrandDto request)
        {
            _validator.ValidateAndThrow(request);
            var brand = _context.Brands.Where(x => x.DeletedAt == null).FirstOrDefault(x => x.Id == request.Id);

            if(brand == null)
            {
                throw new EntityNotFoundException(typeof(Brand));
            }

            brand.Name = request.Name;
            brand.DateModified = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
