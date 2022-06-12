﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
