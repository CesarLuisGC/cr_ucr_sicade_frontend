using Microsoft.AspNetCore.Mvc;
using App.Controllers.Base;

namespace App.Controllers
{
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet("")]
        [Produces("text/html")]
        public IActionResult Index()
        {
            return Utils.View(this, "Html", "Index.html");
        }
    }
}

