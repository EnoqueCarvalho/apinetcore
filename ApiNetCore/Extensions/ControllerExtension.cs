using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiNetCore.Api.Extensions
{
    public static class ControllerExtension
    {
        public static ActionResult TratarExcecao(this ControllerBase controller, Exception exception)
        {
            return controller.StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
        }
    }
}
