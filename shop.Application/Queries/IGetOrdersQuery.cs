using shop.Application.DTOs;
using shop.Application.Interfaces;
using shop.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Queries
{
    public interface IGetOrdersQuery : IQuery<OrderSearch, PagedResponse<ShowOrderDto>>
    {
    }
}
