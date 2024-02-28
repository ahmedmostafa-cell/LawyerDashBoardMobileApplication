using AlMohamyProject.Dtos;
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
    public class LawyerSpecificConsultApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferService offerService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public LawyerSpecificConsultApiController(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager, OfferService OfferService, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            offerService = OfferService;
            _userManager = userManager;
            _accountRepository = accountRepository;

        }
        // GET: api/<LawyerSpecificConsultApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerSpecificConsultApiController>/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> Get(Guid id)
        //{
        //    var user = _userManager.Users.Where(a => a.Id == id.ToString()).FirstOrDefault();
        //    var result = await _accountRepository.LawyerSpecificConsult(user);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest("There is No Data");
        //    }
        //}

        // POST api/<LawyerSpecificConsultApiController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> Post([FromForm] Guid id)
        {
            var user = _userManager.Users.Where(a => a.Id == id.ToString()).FirstOrDefault();
          
            if (user == null)
            {
                return BadRequest("The Lawyer is not registered");
            }
            var result = await _accountRepository.LawyerSpecificConsult(user);
           

            if (result.Value != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new List<WaitingCustomerConsultingDtos>());
            }
        }

        // PUT api/<LawyerSpecificConsultApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerSpecificConsultApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
