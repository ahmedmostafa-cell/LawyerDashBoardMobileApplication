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
    public class LawyerCostConsultApiController : ControllerBase
    {
        private readonly LawyersMainConsultingsService _mainConsultingsService; 
        LawyerPeriodCostConsultService lawyerPeriodCostConsultService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public LawyerCostConsultApiController(LawyersMainConsultingsService mainConsultingsService,LawyerPeriodCostConsultService LawyerPeriodCostConsultService ,SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            _mainConsultingsService = mainConsultingsService;
            lawyerPeriodCostConsultService = LawyerPeriodCostConsultService;
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            Usermanager = usermanager;
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
        }
        // GET: api/<LawyerCostConsultApiController>
        [HttpGet]
        public IEnumerable<TbLawyersMainConsultings> Get()
        {
            List<TbLawyersMainConsultings> lstLogHistories = _mainConsultingsService.getAll().ToList();

            return lstLogHistories;
        }

        // GET api/<LawyerCostConsultApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<TbLawyerPeriodCostConsult> Get(Guid id)
        //{
        //    List<TbLawyerPeriodCostConsult> lstLogHistories = lawyerPeriodCostConsultService.getAll().Where(A => A.LawyerId == id.ToString()).ToList();

        //    return lstLogHistories;
        //}

        // POST api/<LawyerCostConsultApiController>
        [HttpPost]
        public IEnumerable<TbLawyersMainConsultings> Post([FromForm] Guid id)
        {
            List<TbLawyersMainConsultings> lstLogHistories = _mainConsultingsService.getAll().Where(A => A.LawyerId == id.ToString()).ToList();

            return lstLogHistories;
        }

        // PUT api/<LawyerCostConsultApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerCostConsultApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
