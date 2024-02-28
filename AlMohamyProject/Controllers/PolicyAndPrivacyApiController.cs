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
    public class PolicyAndPrivacyApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
      PoliciesAndPrivacyService policyAndPrivacyService;
        AlMohamyDbContext ctx;
        public PolicyAndPrivacyApiController(UserManager<ApplicationUser> usermanager, PoliciesAndPrivacyService PolicyAndPrivacyService, AlMohamyDbContext context)
        {
            policyAndPrivacyService = PolicyAndPrivacyService;
            ctx = context;
            Usermanager = usermanager;

        }
        // GET: api/<PolicyAndPrivacyApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PolicyAndPrivacyApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<TbPoliciesAndPrivacy> Get(string id)
        //{
        //    string userType = Usermanager.Users.Where(a => a.Id == id).FirstOrDefault().UserType;
        //    if (userType == "المستخدم")
        //    {
        //        return policyAndPrivacyService.getAll().Where(a => a.PoliciesAndPrivacyForWhom == "المستخدم").ToList();
        //    }
        //    else
        //    {
        //        return policyAndPrivacyService.getAll().Where(a => a.PoliciesAndPrivacyForWhom == "المحامي").ToList();
        //    }

        //}

        // POST api/<PolicyAndPrivacyApiController>
        [HttpPost]
        public ActionResult<IEnumerable<TbPoliciesAndPrivacy>> Post([FromForm] string id)
        {
            var user = Usermanager.Users.Where(a => a.Id == id).FirstOrDefault();
            if(user == null) 
            {
                return BadRequest("The User Is Nit Registered");

            }
            string userType = user.UserType;
            if (userType == "المستخدم")
            {
                return  policyAndPrivacyService.getAll().Where(a => a.PoliciesAndPrivacyForWhom == "المستخدم").ToList();
            }
            else
            {
                return policyAndPrivacyService.getAll().Where(a => a.PoliciesAndPrivacyForWhom == "المحامي").ToList();
            }
        }

        // PUT api/<PolicyAndPrivacyApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PolicyAndPrivacyApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
