using shop.Application;
using shop.Application.DTOs;
using shop.Application.Exceptions;
using shop.Application.Interfaces;
using shop.Application.Searches;
using shop.Implementation.Commands;
using shop.Implementation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;
        public UsersController(UseCaseExecutor executor, IApplicationActor actor)
        {
            _executor = executor;
            _actor = actor;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] UserSearch search,
            [FromServices] GetUsersQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDto dto,
            [FromServices] CreateUserCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Successfully registered!");
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UserDto dto,
            [FromServices] UpdateUserCommand command)
        {
            dto.Id = id;
            if(_actor.Id != id)
            {
                throw new UnauthorizedUseCaseException(command, _actor);
            }
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] DeleteUserCommand command)
        {
            if (_actor.Id != id)
            {
                throw new UnauthorizedUseCaseException(command, _actor);
            }
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
