using shop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Exceptions
{
    public class UnauthorizedUseCaseException : Exception
    {
        public UnauthorizedUseCaseException(IUseCase useCase, IApplicationActor actor)
            : base($"Actor with an id of {actor.Id} - {actor.Email} tried to execute {useCase.Name}.")
        {

        }
    }
}
