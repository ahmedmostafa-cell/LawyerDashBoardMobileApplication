using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultingEstablishCustomerController : ControllerBase
    {
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public ConsultingEstablishCustomerController(ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;

        }
        // GET: api/<ConsultingEstablishCustomerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ConsultingEstablishCustomerController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbConsultingEstablish> Get(Guid id)
        {
            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).ToList();

            return lstTbConsultingEstablish;
        }

        // POST api/<ConsultingEstablishCustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ConsultingEstablishCustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConsultingEstablishCustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
