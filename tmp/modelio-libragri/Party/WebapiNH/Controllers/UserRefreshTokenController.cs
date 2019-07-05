using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Libragri.PartyDomain.IServices;
using Libragri.PartyDomain.Model;

namespace Libragri.PartyDomain.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRefreshTokenController : ControllerBase
    {
    	private IUserRefreshTokenService _service;
    	
    	public UserRefreshTokenController(IUserRefreshTokenService service)
    	{
    		_service=service;
    	}
    	
        // GET api/[controller]
        [HttpGet]
        public ActionResult<IEnumerable<UserRefreshToken>> Get()
        {
                var result = _service.GetAllAsync().Result;
                return Ok(result);
        }

        // GET api/[controller]/5
        [HttpGet("{id}")]
        public ActionResult<UserRefreshToken> Get(Guid id)
        {
                var result = _service.GetByIdAsync(id).Result;
                return Ok(result);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<UserRefreshToken> Post([FromBody] UserRefreshToken entity)
        {
                var result = _service.AddAsync(entity).Result;
                return Ok(result);
        }

        // PUT api/[controller]/5
        [HttpPut("{id}")]
        public ActionResult<UserRefreshToken> Put([FromBody] UserRefreshToken entity)
        {
                var result = _service.UpdateAsync(entity).Result;
                return Ok(result);
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
                _service.DeleteAsync(id).Wait();
        }
    }
}
