using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMainConsultingApiController : ControllerBase
    {
        MainConsultingService mainConsultingService;
       SubMainConsultingService _subMainConsultingService;
        AlMohamyDbContext ctx;
        public SubMainConsultingApiController(SubMainConsultingService subMainConsultingService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            mainConsultingService = MainConsultingService;
            ctx = context;
            _subMainConsultingService = subMainConsultingService;

        }
        // GET: api/<SubMainConsultingApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SubMainConsultingApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<TbSubMainConsulting> Get(string id)
        //{
           
        //        return _subMainConsultingService.getAll().Where(a => a.MainConsultingId == Guid.Parse(id)).ToList();
           

        //}

        // POST api/<SubMainConsultingApiController>
        [HttpPost]
        public IEnumerable<TbSubMainConsulting> Post([FromForm] string id)
        {
            return _subMainConsultingService.getAll().Where(a => a.MainConsultingId == Guid.Parse(id)).ToList();
        }

        // PUT api/<SubMainConsultingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubMainConsultingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
