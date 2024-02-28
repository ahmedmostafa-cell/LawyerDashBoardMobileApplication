using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityApiController : ControllerBase
    {
        CityService cityService;
        AlMohamyDbContext ctx;
        public CityApiController(CityService CityService, AlMohamyDbContext context)
        {
            cityService = CityService;
            ctx = context;

        }
        // GET: api/<CityApiController>
        [HttpGet]
        public IEnumerable<TbCity> Get()
        {
            List<TbCity> lstCities = cityService.getAll();

            return lstCities;
        }

        // GET api/<CityApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CityApiController>
        [HttpPost]
        public IEnumerable<TbCity> Post([FromForm] string userid)
        {
            List<TbCity> lstCities = cityService.getAll();

            return lstCities;
        }

        // PUT api/<CityApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CityApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
