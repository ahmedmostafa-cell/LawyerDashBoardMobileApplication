using AlMohamyProject.Dtos;
using BL;
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
    public class LawyerDelegationDataApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        OfferService offerService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;

        private readonly UserManager<ApplicationUser> _userManager;

        public LawyerDelegationDataApiController(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager, OfferService OfferService, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            _userManager = userManager;
            offerService = OfferService;
            _accountRepository = accountRepository;

        }
        // GET: api/<LawyerDelegationDataApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerDelegationDataApiController>/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> Get(Guid id)
        //{
        //    var user = _userManager.Users.Where(a => a.Id == id.ToString()).FirstOrDefault();

        //    var result = await _accountRepository.LawyerDelegationData(user);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest("There is No Data");
        //    }
        //}

        // POST api/<LawyerDelegationDataApiController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> Post([FromForm] Guid id)
        {
            var user = _userManager.Users.Where(a => a.Id == id.ToString()).FirstOrDefault();

            var result = await _accountRepository.LawyerDelegationData(user);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new List<WaitingCustomerConsultingDtos>());
            }
        }

        // PUT api/<LawyerDelegationDataApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerDelegationDataApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
