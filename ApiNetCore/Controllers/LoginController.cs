using ApiNetCore.Api.Extensions;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult> PostLogin(LoginDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else if (user == null)
                return BadRequest();

            try
            {
                var result = await loginService.FindByLoginAsync(user);

                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception exception)
            {
                return this.TratarExcecao(exception);
            }
        }
    }
}
