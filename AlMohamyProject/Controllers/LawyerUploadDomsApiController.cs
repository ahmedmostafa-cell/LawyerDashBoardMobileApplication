using AlMohamyProject.Models;
using BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerUploadDomsApiController : ControllerBase
    {

        UserManager<ApplicationUser> Usermanager;
        private readonly IAccountRepository _accountRepository;
        public LawyerUploadDomsApiController(UserManager<ApplicationUser> usermanager, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            Usermanager = usermanager;
        }
        // GET: api/<LawyerUploadDomsApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerUploadDomsApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LawyerUploadDomsApiController>
        [HttpPost("uploadDoms")]
        public async Task<ActionResult<ApplicationUser>> uploadDoms([FromForm] SignUpModel signUpModel)
        {
            var result = await _accountRepository.uploadDoms(signUpModel);

            if (result == null)
            {
                return BadRequest("The Files Are Not Saved");

            }
            else
            {
               

                return Ok(result);

            }

        }

        // PUT api/<LawyerUploadDomsApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerUploadDomsApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
