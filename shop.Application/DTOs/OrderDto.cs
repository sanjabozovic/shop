﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.DTOs
{
    public class OrderDto
    {
        public IEnumerable<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();
    }

    public class OrderLineDto
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
