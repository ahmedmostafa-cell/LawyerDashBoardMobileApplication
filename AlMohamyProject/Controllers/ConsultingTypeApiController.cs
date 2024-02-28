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
    public class ConsultingTypeApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        ConsultingTypeService consultingTypeService;
        AlMohamyDbContext ctx;
        public ConsultingTypeApiController(UserManager<ApplicationUser> usermanager, ConsultingTypeService ConsultingTypeService, AlMohamyDbContext context)
        {
            consultingTypeService = ConsultingTypeService;
            ctx = context;
            Usermanager = usermanager;

        }
        // GET: api/<ConsultingTypeApiController>
        [HttpGet]
        public IEnumerable<TbConsultingType> Get()
        {
            return consultingTypeService.getAll();
        }

        // GET api/<ConsultingTypeApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ConsultingTypeApiController>
        [HttpPost]
        public IEnumerable<TbConsultingType> Post([FromForm] string userid)
        {
            return consultingTypeService.getAll();
        }

        // PUT api/<ConsultingTypeApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConsultingTypeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
