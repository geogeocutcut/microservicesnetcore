using System.Threading.Tasks;
using Libragri.AuthenticationDomain.Webapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Libragri.AuthenticationDomain.WebapiNH.Controllers
{
    [ApiController]

    public class HealthController:ControllerBase
    {
        [Route("health")]
        [HttpGet]
        public async Task<IActionResult> HealthAsync()
        {
            return Ok(new HealthStatus{Status=HealthStatusEnum.Ok.ToString(),Message="Tout va bien !!"});
        }
    }
}