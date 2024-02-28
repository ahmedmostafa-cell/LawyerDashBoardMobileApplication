using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchByConsultationApiController : ControllerBase
    {
        MainConsultingService mainConsultingService;

        AlMohamyDbContext ctx;
        public SearchByConsultationApiController(MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {

            ctx = context;
            mainConsultingService = MainConsultingService;

        }
        // GET: api/<SearchByConsultationApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SearchByConsultationApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SearchByConsultationApiController>
        [HttpPost]
        public ActionResult<IEnumerable<TbMainConsulting>> Post([FromForm] string id)
        {
            var mainConsulting = mainConsultingService.getAll().Where(a => a.MainConsultingTitle.Contains(id)).ToList();
            
           
            if (mainConsulting != null)
            {
                return mainConsulting;
            }
            else
            {
                return BadRequest(mainConsulting);
            }
        }

        // PUT api/<SearchByConsultationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchByConsultationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
