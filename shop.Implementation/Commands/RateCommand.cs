using shop.Application.Commands;
using shop.Application.DTOs;
using shop.Application.Interfaces;
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
    public class RateCommand : IRateCommand
    {
        private readonly ShopContext _context;
        private readonly IApplicationActor _actor;
        private readonly RateValidator _validator;

        public RateCommand(ShopContext context, IApplicationActor actor, RateValidator validator)
        {
            _context = context;
            _actor = actor;
            _validator = validator;
        }

        public int Id => 20;

        public string Name => "Rate product";

        public void Execute(RateDto request)
        {
            _validator.ValidateAndThrow(request);

            var newRating = new Rating
            {
                ProductId = request.ProductId,
                UserId = _actor.Id,
                RatingValue = request.Rating
            };

            _context.Ratings.Add(newRating);
            _context.SaveChanges();
        }
    }
}
