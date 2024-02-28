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
    public class AreaApiController : ControllerBase
    {
        AreaService areaService;
        AlMohamyDbContext ctx;
        public AreaApiController(AreaService AreaService, AlMohamyDbContext context)
        {
            areaService = AreaService;
            ctx = context;

        }
        // GET: api/<AreaApiController>
        [HttpGet]
        public IEnumerable<TbArea> Get()
        {
            List<TbArea> lstAreas = areaService.getAll();

            return lstAreas;
        }

        // GET api/<AreaApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<TbArea> Get(Guid id)
        //{
        //    List<TbArea> lstAreas = areaService.getAll().Where(a=> a.CityId == id).ToList();

        //    return lstAreas;
        //}

        // POST api/<AreaApiController>
        [HttpPost]
        public IEnumerable<TbArea> Post([FromForm] Guid id)
        {
            List<TbArea> lstAreas = areaService.getAll().Where(a => a.CityId == id).ToList();

            return lstAreas;
        }

        // PUT api/<AreaApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AreaApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
