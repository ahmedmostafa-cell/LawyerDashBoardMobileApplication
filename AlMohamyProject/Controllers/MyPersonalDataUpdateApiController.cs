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
    public class MyPersonalDataUpdateApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        private readonly IAccountRepository _accountRepository;
        public MyPersonalDataUpdateApiController(UserManager<ApplicationUser> usermanager, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            Usermanager = usermanager;
        }
        // GET: api/<MyPersonalDataUpdateApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MyPersonalDataUpdateApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MyPersonalDataUpdateApiController>
        [HttpPost("UpdatePersonalData")]
        public async Task<ActionResult<ApplicationUser>> UpdatePersonalData([FromForm] SignUpModel signUpModel)
        {
            var result = await _accountRepository.UpdatePersonalData(signUpModel);

            if (result == null)
            {
                return BadRequest("The Files Are Not Saved Or You Have Not Attach Any Files");

            }
            else
            {


                return Ok(result);

            }

        }

        // PUT api/<MyPersonalDataUpdateApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MyPersonalDataUpdateApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
