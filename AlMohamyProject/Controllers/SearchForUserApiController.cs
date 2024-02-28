using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchForUserApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        UserManager<ApplicationUser> Usermanager;
        EvaluationService evaluationService;
        AlMohamyDbContext ctx;
        public SearchForUserApiController(IAccountRepository accountRepository, EvaluationService EvaluationService, UserManager<ApplicationUser> usermanager, AlMohamyDbContext context)
        {
            evaluationService = EvaluationService;
            ctx = context;
            Usermanager = usermanager;
            _accountRepository = accountRepository;

        }
        // GET: api/<SearchForUserApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SearchForUserApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SearchForUserApiController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> PostAsync([FromForm] string id)
        {
          
            var result = await _accountRepository.SearchForUser(id);
            if (result.Value != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new List<WaitingCustomerConsultingDtos>());
            }
        }

        // PUT api/<SearchForUserApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchForUserApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
