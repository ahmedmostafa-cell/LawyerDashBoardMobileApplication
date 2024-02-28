using BL;
using BL.Migrations;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentActiveUsersApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        AboutAppService aboutAppService;
        AlMohamyDbContext ctx;
        public CurrentActiveUsersApiController(UserManager<ApplicationUser> usermanager, AboutAppService AboutAppService, AlMohamyDbContext context)
        {
            aboutAppService = AboutAppService;
            ctx = context;
            Usermanager = usermanager;

        }
        // GET: api/<CurrentActiveUsersApiController>
        [HttpGet]
        public async Task<List<TbActivityLog>> GetRecentActivityLogsAsync()
        {
            List <TbActivityLog> l = ctx.TbActivityLogs.Where(a => a.Timestamp > DateTime.Now.AddMinutes(-10)).ToList();
            return ctx.TbActivityLogs.Where(a=> a.Timestamp > DateTime.Now.AddMinutes(-10)).ToList();
        }

        // GET api/<CurrentActiveUsersApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CurrentActiveUsersApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CurrentActiveUsersApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CurrentActiveUsersApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
