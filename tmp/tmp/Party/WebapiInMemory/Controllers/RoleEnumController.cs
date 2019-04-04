using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Libragri.partyDomain.IServices;
using Libragri.partyDomain.Model;

namespace Libragri.partyDomain.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleEnumController : ControllerBase
    {
    	private IRoleEnumService _service;
    	
    	public RoleEnumController(IRoleEnumService service)
    	{
    		_service=service;
    	}
    	
        // GET api/[controller]
        [HttpGet]
        public ActionResult<IEnumerable<RoleEnum>> Get()
        {
                var result = _service.GetAllAsync().Result;
                return Ok(result);
        }

        // GET api/[controller]/5
        [HttpGet("{id}")]
        public ActionResult<RoleEnum> Get(Guid id)
        {
                var result = _service.GetByIdAsync(id).Result;
                return Ok(result);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<RoleEnum> Post([FromBody] RoleEnum entity)
        {
                var result = _service.AddAsync(entity).Result;
                return Ok(result);
        }

        // PUT api/[controller]/5
        [HttpPut("{id}")]
        public ActionResult<RoleEnum> Put([FromBody] RoleEnum entity)
        {
                var result = _service.UpdateAsync(entity).Result;
                return Ok(result);
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
                _service.DeleteAsync(id);
        }
    }
}
