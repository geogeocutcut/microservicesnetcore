using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebapiNH.Controllers
{
    [ApiController]

    public class HealthController:ControllerBase
    {
        [Route("health")]
        [HttpGet]
        public async Task<IActionResult> HealthAsync()
        {
            return Ok("tout va bien");
        }
    }
}