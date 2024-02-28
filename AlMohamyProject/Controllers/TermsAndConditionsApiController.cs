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
    public class TermsAndConditionsApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
       TermAndConditionService termAndConditionService;
        public TermsAndConditionsApiController(UserManager<ApplicationUser> usermanager, TermAndConditionService TermAndConditionService)
        {
            termAndConditionService = TermAndConditionService;
            Usermanager = usermanager;
        }
        // GET: api/<TermsAndConditionsApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TermsAndConditionsApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<TbTermAndCondition> Get(string id)
        //{
        //    string userType = Usermanager.Users.Where(a=> a.Id == id).FirstOrDefault().UserType;
        //    if(userType == "المستخدم") 
        //    {
        //        return termAndConditionService.getAll().Where(a=> a.TermsAndConditionsForWhom == "المستخدم").ToList();
        //    }
        //    else 
        //    {
        //        return termAndConditionService.getAll().Where(a => a.TermsAndConditionsForWhom == "المحامي").ToList();
        //    }
            
        //}

        // POST api/<TermsAndConditionsApiController>
        [HttpPost]
        public ActionResult<IEnumerable<TbTermAndCondition>> Post([FromForm] string id)
        {
            var user = Usermanager.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("The User Is Nit Registered");

            }
            string userType = user.UserType;
            if (userType == "المستخدم")
            {
                return termAndConditionService.getAll().Where(a => a.TermsAndConditionsForWhom == "المستخدم").ToList();
            }
            else
            {
                return termAndConditionService.getAll().Where(a => a.TermsAndConditionsForWhom == "المحامي").ToList();
            }

        }

        // PUT api/<TermsAndConditionsApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TermsAndConditionsApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
