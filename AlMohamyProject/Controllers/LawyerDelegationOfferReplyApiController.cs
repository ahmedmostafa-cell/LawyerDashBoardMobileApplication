using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerDelegationOfferReplyApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public LawyerDelegationOfferReplyApiController(IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
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
        // GET: api/<LawyerDelegationOfferReplyApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerDelegationOfferReplyApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LawyerDelegationOfferReplyApiController>
        [HttpPost("replyDelegationOffer")]
        public async Task<IActionResult> replyDelegationOffer([FromForm] ConsultingEstablishDtos model)
        {
            

            var result = await _accountRepository.replyDelegationOffer(model);

            if (result.Value.MyString == "لم يتم  الرد عل طلب عرض سعر للتفويض")
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }

        // PUT api/<LawyerDelegationOfferReplyApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerDelegationOfferReplyApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
