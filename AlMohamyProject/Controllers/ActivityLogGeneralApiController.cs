using BL;
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
    public class ActivityLogGeneralApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        ActivityLogService activityLogService;
        AlMohamyDbContext ctx;
        public ActivityLogGeneralApiController(ActivityLogService ActivityLogService, UserManager<ApplicationUser> usermanager, AlMohamyDbContext context)
        {
            activityLogService = ActivityLogService;
            ctx = context;
            Usermanager = usermanager;

        }
        // GET: api/<ActivityLogGeneralApiController>
        [HttpGet]
        public async Task<List<TbActivityLog>> Get()
        {
            List<TbActivityLog> lstActivityLogs = ctx.TbActivityLogs.Where(a => a.Timestamp > DateTime.Now.AddMinutes(-10)).ToList();

            foreach (var i in lstActivityLogs)
            {
                if (i.UserName == null)
                {
                    i.UserName = Usermanager.Users.Where(a => a.Id == i.UserId).FirstOrDefault().UserName;
                    activityLogService.Edit(i);
                }
                if (i.CreatedBy == null)
                {
                    i.CreatedBy = Usermanager.Users.Where(a => a.Id == i.UserId).FirstOrDefault().UserType;
                    activityLogService.Edit(i);
                }


            }
            lstActivityLogs = lstActivityLogs.Where(a => a.Timestamp > DateTime.Now.AddDays(-14)).ToList();
            return lstActivityLogs;
        }

        // GET api/<ActivityLogGeneralApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ActivityLogGeneralApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActivityLogGeneralApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ActivityLogGeneralApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
