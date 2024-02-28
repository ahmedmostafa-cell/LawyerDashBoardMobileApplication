using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainConsultingApiController : ControllerBase
    {
      
       MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public MainConsultingApiController(MainConsultingService MainConsultingService,  AlMohamyDbContext context)
        {
            mainConsultingService = MainConsultingService;
            ctx = context;
           
        }
        // GET: api/<MainConsultingApiController>
        [HttpGet]
        public IEnumerable<TbMainConsulting> Get()
        {
            return mainConsultingService.getAll();
        }

        // GET api/<MainConsultingApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MainConsultingApiController>
        [HttpPost]
        public IEnumerable<TbMainConsulting> Post([FromForm] string userid)
        {
            return mainConsultingService.getAll();
        }

        // PUT api/<MainConsultingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MainConsultingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
