using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Libragri.AuthenticationDomain.IServices;
using Libragri.AuthenticationDomain.Model;

namespace Libragri.AuthenticationDomain.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPwdRequestController : ControllerBase
    {
    	private IResetPwdRequestService _service;
    	
    	public ResetPwdRequestController(IResetPwdRequestService service)
    	{
    		_service=service;
    	}
    	
        // GET api/[controller]
        [HttpGet]
        public ActionResult<IEnumerable<ResetPwdRequest>> Get()
        {
                var result = _service.GetAllAsync().Result;
                return Ok(result);
        }

        // GET api/[controller]/5
        [HttpGet("{id}")]
        public ActionResult<ResetPwdRequest> Get(Guid id)
        {
                var result = _service.GetByIdAsync(id).Result;
                return Ok(result);
        }

        // POST api/[controller]
        [HttpPost]
        public ActionResult<ResetPwdRequest> Post([FromBody] ResetPwdRequest entity)
        {
                var result = _service.AddAsync(entity).Result;
                return Ok(result);
        }

        // PUT api/[controller]/5
        [HttpPut("{id}")]
        public ActionResult<ResetPwdRequest> Put([FromBody] ResetPwdRequest entity)
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
