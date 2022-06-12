using AutoMapper;
using shop.Application.DTOs;
using shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Profiles
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<UseCaseLog, LogDto>();
        }
    }
}
