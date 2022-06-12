using AutoMapper;
using shop.Application.DTOs;
using shop.Application.Exceptions;
using shop.Application.Interfaces;
using shop.Application.Queries;
using shop.Application.Searches;
using shop.DataAccess;
using shop.Domain;
using shop.Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Queries
{
    public class GetOrdersQuery : IGetOrdersQuery
    {
        private readonly ShopContext _context;
        private readonly IApplicationActor _actor;
        private readonly IMapper _mapper;

        public GetOrdersQuery(ShopContext context, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _mapper = mapper;
            _actor = actor;
        }
        public int Id => 17;

        public string Name => "Get orders";

        public PagedResponse<ShowOrderDto> Execute(OrderSearch search)
        {
            var query = _context.Orders.Include(x => x.OrderLines).Where(x => x.DeletedAt == null).AsQueryable();

            if (search.PlacedAt.HasValue)
            {
                query = query.Where(x => x.PlacedAt == search.PlacedAt);
            }

            if(!string.IsNullOrEmpty(search.ProductName) && !string.IsNullOrWhiteSpace(search.ProductName))
            {
                search.ProductName = search.ProductName.ToLower();

                query = query.Where(x => x.OrderLines.Any(y => y.ProductName.Contains(search.ProductName)));
            }

            var user = _context.Users.Find(_actor.Id);

            if(user.RoleId == 2)
            {
                query = query.Where(x => x.UserId == user.Id);
            }

            var orders = query.Paged<ShowOrderDto, Order>(search, _mapper);

            if (orders.Items.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            return orders;
        }
    }
}
