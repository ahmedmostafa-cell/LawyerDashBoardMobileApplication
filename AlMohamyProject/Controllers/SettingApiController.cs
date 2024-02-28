using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingApiController : ControllerBase
    {
        SettingService settingService;
        AlMohamyDbContext ctx;
        public SettingApiController(SettingService SettingService, AlMohamyDbContext context)
        {
            settingService = SettingService;
            ctx = context;

        }
        // GET: api/<SettingApiController>
        [HttpGet]
        public TbSetting Get()
        {
            TbSetting item = settingService.getAll().First();

            return item;
        }

        // GET api/<SettingApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SettingApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SettingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SettingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
