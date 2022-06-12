using AutoMapper;
using shop.Application.Commands;
using shop.Application.DTOs;
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
    public class CreateBrandCommand : ICreateBrandCommand
    {
        private readonly ShopContext _context;
        private readonly CreateBrandValidator _validator;
        private readonly IMapper _mapper;

        public CreateBrandCommand(ShopContext context, CreateBrandValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Insert brand";

        public void Execute(BrandDto request)
        {
            _validator.ValidateAndThrow(request);
            var brand = _mapper.Map<Brand>(request);
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }
    }
}
