using shop.Application.DTOs;
using shop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Commands
{
    public interface IRateCommand : ICommand<RateDto>
    {
    }
}
