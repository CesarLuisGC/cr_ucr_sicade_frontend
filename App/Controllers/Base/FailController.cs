using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Entities.Models.Base;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FailController : ControllerBase
    {
        public FailController()
        {
        }

        [Route("Report")]
        public IActionResult Report()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return BadRequest(new Response()
            {
                state = context.Error.Message.Contains("SHOW:") ? 1 : -1,
                message = GenerarMensaje(context.Error.Message),
                data = null
            });
        }

        [Route("Report-Develop")]
        public IActionResult ReportDevelop([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return BadRequest(new Response()
            {
                state = context.Error.Message.Contains("SHOW:") ? 1 : -1,
                message = GenerarMensaje(context.Error.Message, true),
                data = null
            });
        }

        private string GenerarMensaje(string msj, bool develop = false)
        {
            if (msj.Contains("sqlerror") && msj.Contains("SHOW: "))
            {
                Error error = JsonConvert.DeserializeObject<Error>(msj)!;

                msj = error.message.Replace("SHOW: ", "");
            }
            else
            {
                msj = "Internal Server Error: " + (develop ? "[" + msj + "]" : "");
            }

            return msj;
        }
    }
}
