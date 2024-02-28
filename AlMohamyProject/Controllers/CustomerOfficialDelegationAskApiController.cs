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
    public class CustomerOfficialDelegationAskApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public CustomerOfficialDelegationAskApiController(IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
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
        // GET: api/<CustomerOfficialDelegationAskApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CustomerOfficialDelegationAskApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerOfficialDelegationAskApiController>
        [HttpPost("CustomerOfficialDelegationAsk")]
        public async Task<IActionResult> CustomerOfficialDelegationAsk([FromForm] ConsultingEstablishDtos model)
        {


            var result = await _accountRepository.CustomerOfficialDelegationAsk(model);

            if (result.Value.MyString == "لم يتم  ارسال طلب  للتفويض")
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }

        // PUT api/<CustomerOfficialDelegationAskApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerOfficialDelegationAskApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
