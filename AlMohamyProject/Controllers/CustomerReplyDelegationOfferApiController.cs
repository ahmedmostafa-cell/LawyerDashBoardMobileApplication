using AlMohamyProject.Dtos;
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
    public class CustomerReplyDelegationOfferApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public CustomerReplyDelegationOfferApiController(IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            Usermanager = usermanager;
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
            settingService = SettingService;
            _accountRepository = accountRepository;
        }
        // GET: api/<CustomerReplyDelegationOfferApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CustomerReplyDelegationOfferApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerReplyDelegationOfferApiController>
        [HttpPost("customerReplyDelegationOffer")]
        public async Task<IActionResult> customerReplyDelegationOffer([FromForm] ConsultingEstablishDtos model)
        {


            var result = await _accountRepository.customerReplyDelegationOffer(model);

            if (result.Value.MyString == "لم يتم  الرد عل عرض سعر للتفويض")
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }

        // PUT api/<CustomerReplyDelegationOfferApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerReplyDelegationOfferApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
