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
    public class AboutAppApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        AboutAppService aboutAppService;
        AlMohamyDbContext ctx;
        public AboutAppApiController(UserManager<ApplicationUser> usermanager, AboutAppService AboutAppService, AlMohamyDbContext context)
        {
            aboutAppService = AboutAppService;
            ctx = context;
            Usermanager = usermanager;

        }
        // GET: api/<AboutAppApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AboutAppApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<TbAboutApp> Get(string id)
        //{
        //    string userType = Usermanager.Users.Where(a => a.Id == id).FirstOrDefault().UserType;
        //    if (userType == "المستخدم")
        //    {
        //        return aboutAppService.getAll().Where(a => a.AboutAppForWhom == "المستخدم").ToList();
        //    }
        //    else
        //    {
        //        return aboutAppService.getAll().Where(a => a.AboutAppForWhom == "المحامي").ToList();
        //    }

        //}

        // POST api/<AboutAppApiController>
        [HttpPost]
        public IEnumerable<TbAboutApp> Post([FromForm] string id)
        {
            string userType = Usermanager.Users.Where(a => a.Id == id).FirstOrDefault().UserType;
            if (userType == "المستخدم")
            {
                return aboutAppService.getAll().Where(a => a.AboutAppForWhom == "المستخدم").ToList();
            }
            else
            {
                return aboutAppService.getAll().Where(a => a.AboutAppForWhom == "المحامي").ToList();
            }
        }

        // PUT api/<AboutAppApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AboutAppApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
