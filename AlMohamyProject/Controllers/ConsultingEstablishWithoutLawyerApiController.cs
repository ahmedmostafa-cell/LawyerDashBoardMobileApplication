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
    public class ConsultingEstablishWithoutLawyerApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public ConsultingEstablishWithoutLawyerApiController(IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
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
        // GET: api/<ConsultingEstablishWithoutLawyerApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ConsultingEstablishWithoutLawyerApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ConsultingEstablishWithoutLawyerApiController>
        [HttpPost("EstablishConsultation")]
        public async Task<IActionResult> EstablishConsultation([FromForm] ConsultingEstablishDtos model)
        {

            var result = await _accountRepository.EstablishConsultWithoutLawyer(model);

            if (result == null)
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {
                if(result.Value.ConsultingId == null) 
                {
                    return BadRequest("The Consultation Is Not Saved As The Propsed Customer Pay Is Less Than The Valid One (50 SAR)");
                }
                else 
                {
                    return Ok(result);
                }


              

            }




        }


        [HttpPost("EstablishConsultation2")]
        public async Task<IActionResult> EstablishConsultation2([FromBody] ConsultingEstablishDtos model)
        {

            var result = await _accountRepository.EstablishConsultWithoutLawyer(model);

            if (result == null)
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }

        // PUT api/<ConsultingEstablishWithoutLawyerApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConsultingEstablishWithoutLawyerApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
