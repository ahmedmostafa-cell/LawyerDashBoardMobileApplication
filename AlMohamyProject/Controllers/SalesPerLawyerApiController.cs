using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPerLawyerApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public SalesPerLawyerApiController(IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
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
        // GET: api/<SalesPerLawyerApiController>
        [HttpGet]
        public IEnumerable<TbConsultingEstablish> Get()
        {
            List<TbConsultingEstablish> lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).ToList();

            return lstLogHistories;
        }

        // GET api/<SalesPerLawyerApiController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbConsultingEstablish> Get(Guid id)
        {
            List<TbConsultingEstablish> lstLogHistories = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).ToList();

            return lstLogHistories;
        }
        [HttpGet("{id}/{optionDate1}/{optionDate2}")]
        public IEnumerable<TbConsultingEstablish> Get(Guid id, string optionDate1, string optionDate2)
        {
            List<TbConsultingEstablish> lstLogHistories = new List<TbConsultingEstablish>();
            if (id != null && optionDate2 == null && optionDate1 == null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).Where(A => A.LawyerId == id.ToString()).ToList();
            }
            else if (id != null && optionDate2 != null && optionDate1 == null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).Where(A => A.LawyerId == id.ToString()).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (id != null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).Where(A => A.LawyerId == id.ToString()).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 == null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).ToList();
            }
            else if (id == null && optionDate2 != null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id != null && optionDate2 != null && optionDate1 != null)
            {
                lstLogHistories = consultingEstablishService.getAll().Where(a => a.TheConsultingPaidValue != null || a.CreatedBy != null || a.TheDocumentationPaidValue != null).Where(A => A.LawyerId == id.ToString()).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }



            return lstLogHistories;
        }
        // POST api/<SalesPerLawyerApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SalesPerLawyerApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SalesPerLawyerApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
