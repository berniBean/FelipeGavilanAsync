using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApiPruebas.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SaludosController : ControllerBase
    {
        [HttpGet("{nombre}")]
        public ActionResult<string> Index(string nombre)
        {
            return $"Hola, {nombre}!";
        }
    }
}
