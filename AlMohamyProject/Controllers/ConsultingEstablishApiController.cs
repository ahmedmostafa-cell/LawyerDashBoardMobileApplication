using AlMohamyProject.Dtos;
using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultingEstablishApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public ConsultingEstablishApiController(IAccountRepository accountRepository,SettingService SettingService,SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
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
        // GET: api/<ConsultingEstablishApiController>
        [HttpGet]
        public IEnumerable<TbConsultingEstablish> Get()
        {
            List<TbConsultingEstablish> lstLogHistories = consultingEstablishService.getAll().ToList();

            return lstLogHistories;
        }


        [HttpGet("{id}")]
        public IEnumerable<TbConsultingEstablish> Get(Guid id)
        {
            List<TbConsultingEstablish> lstLogHistories = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).ToList();

            return lstLogHistories;
        }

        // GET api/<ConsultingEstablishApiController>/5
        [HttpGet("{id}/{optionDate1}/{optionDate2}")]
        public IEnumerable<TbConsultingEstablish> Get(Guid id, string optionDate1, string optionDate2)
        {
            List<TbConsultingEstablish> lstLogHistories = new List<TbConsultingEstablish>();
            if (id != null && optionDate2 == null && optionDate1 == null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).ToList();
            }
            else if (id != null && optionDate2 != null && optionDate1 == null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (id != null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 == null)
            {
                lstLogHistories = consultingEstablishService.getAll().ToList();
            }
            else if (id == null && optionDate2 != null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id != null && optionDate2 != null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }



            return lstLogHistories;
        }

        // POST api/<ConsultingEstablishApiController>

        [HttpPost("EstablishConsultation")]
        public async Task<IActionResult> EstablishConsultation([FromForm] ConsultingEstablishDtos model)
        {

            var result = await _accountRepository.EstablishConsult(model);

            if (result.Value.MyString == "لم يتم  حجز الاستشارة")
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }



        [HttpPost("CheckBeforeEstablishConsultation")]
        public async Task<IActionResult> CheckBeforeEstablishConsultation([FromForm] ConsultingEstablishDtos model)
        {

            var result = await _accountRepository.CheckBeforeEstablishConsult(model);

            if (result.Value.MyString == "لم يتم  حجز الاستشارة")
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {


                return Ok(result.Value.MyString);

            }




        }



        [HttpPost("EstablishConsultation2")]
        public async Task<IActionResult> EstablishConsultation2([FromBody] ConsultingEstablishDtos model)
        {

            var result = await _accountRepository.EstablishConsult(model);

            if (result == null)
            {
                return BadRequest("The Consultation Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }


       

        // PUT api/<ConsultingEstablishApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConsultingEstablishApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
