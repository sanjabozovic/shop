using shop.Application.Interfaces;
using shop.DataAccess;
using shop.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Implementation.Logging
{
	public class DatabaseUseCaseLogger : IUseCaseLogger
	{
        private readonly ShopContext _context;

        public DatabaseUseCaseLogger(ShopContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object useCaseData)
        {
            _context.UseCaseLogs.Add(new UseCaseLog
            {
                UserId = actor.Id,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(useCaseData),
                Date = DateTime.UtcNow,
                Email = actor.Email
            });

            _context.SaveChanges();
        }
    }
}
