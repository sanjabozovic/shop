using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain
{
    public class Rating 
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int RatingValue { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
