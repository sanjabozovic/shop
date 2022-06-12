using AutoMapper;
using shop.Application.DTOs;
using shop.Application.Exceptions;
using shop.Application.Queries;
using shop.Application.Searches;
using shop.DataAccess;
using shop.Domain;
using shop.Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Queries
{
    public class GetUsersQuery : IGetUsersQuery
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public GetUsersQuery(ShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 18;

        public string Name => "Get users";

        public PagedResponse<UserDto> Execute(UserSearch search)
        {
            var query = _context.Users.Where(x => x.DeletedAt == null).AsQueryable();

            if(!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
            {
                search.Keyword = search.Keyword.ToLower();

                query = query.Where(x => x.FirstName.ToLower().Contains(search.Keyword) ||
                x.LastName.ToLower().Contains(search.Keyword) || x.Email.ToLower().Contains(search.Keyword));
            }

            var users = query.Paged<UserDto, User>(search, _mapper);

            if (users.Items.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            return users;
        }
    }
}
