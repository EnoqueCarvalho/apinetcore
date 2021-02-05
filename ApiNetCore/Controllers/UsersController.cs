using ApiNetCore.Api.Extensions;
using Domain.Entities;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiNetCore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await userService.GetAll());
            }
            catch (Exception exception)
            {
                return this.TratarExcecao(exception);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await userService.Get(id));
            }
            catch (Exception exception)
            {
                return this.TratarExcecao(exception);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserEntity user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await userService.Post(user);

                if (result != null)
                    return Ok(await userService.Get(result.Id));

                return BadRequest();
            }
            catch (Exception exception)
            {
                return this.TratarExcecao(exception);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(UserEntity user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await userService.Put(user);

                if (result != null)
                    return Ok(result);

                return BadRequest();
            }
            catch (Exception exception)
            {
                return this.TratarExcecao(exception);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();

                if (await userService.Delete(id))
                    return Ok();

                return NotFound();
            }
            catch (Exception exception)
            {
                return this.TratarExcecao(exception);
            }
        }
    }
}
