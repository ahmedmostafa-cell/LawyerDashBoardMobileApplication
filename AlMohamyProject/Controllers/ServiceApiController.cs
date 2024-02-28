using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceApiController : ControllerBase
    {
        ServicesService serviceservice;
        AlMohamyDbContext ctx;
        public ServiceApiController(ServicesService Serviceservice, AlMohamyDbContext context)
        {
            serviceservice = Serviceservice;
            ctx = context;

        }
        // GET: api/<ServiceApiController>
        [HttpGet]
        public IEnumerable<TbServices> Get()
        {
            List<TbServices> lstServices = serviceservice.getAll();

            return lstServices;
        }

        // GET api/<ServiceApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ServiceApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ServiceApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
