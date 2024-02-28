using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerMainConsultingApiController : ControllerBase
    {
        private readonly LawyersMainConsultingsService lawyersMainConsultingsService;
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public LawyerMainConsultingApiController(LawyersMainConsultingsService LawyersMainConsultingsService,IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            Usermanager = usermanager;
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
            settingService = SettingService;
            _accountRepository = accountRepository;
            lawyersMainConsultingsService = LawyersMainConsultingsService;
        }
        // GET: api/<LawyerMainConsultingApiController>
        [HttpGet]
        public IEnumerable<TbLawyersMainConsultings> Get()
        {
            List<TbLawyersMainConsultings> lstLawyersMainConsultings = lawyersMainConsultingsService.getAll().ToList();

            return lstLawyersMainConsultings;
        }

        // GET api/<LawyerMainConsultingApiController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbLawyersMainConsultings> Get(Guid id)
        {
            List<TbLawyersMainConsultings> lstLawyersMainConsultings = lawyersMainConsultingsService.getAll().Where(A => A.LawyerId == id.ToString()).ToList();

            return lstLawyersMainConsultings;
        }

        // POST api/<LawyerMainConsultingApiController>
        [HttpPost("SendLawyerConsultWCostWTime")]
        public async Task<IActionResult> PostAsync([FromBody] LawyerConsultDtos model)
        {

            string highestconsult30min = settingService.getAll().FirstOrDefault().Consulting30MinutesCost;
            string highestconsult60min = settingService.getAll().FirstOrDefault().Consulting60MinutesCost;
            string highestconsult90min = settingService.getAll().FirstOrDefault().Consulting90MinutesCost;
            var user = Usermanager.Users.Where(a => a.Id == model.LawyerId).FirstOrDefault();
            var oldlawyermainconsult = lawyersMainConsultingsService.getAll().Where(a => a.LawyerId == user.Id).ToList();
            foreach(var i in oldlawyermainconsult) 
            {
                foreach(var el in model.LawyerConsultDetails) 
                {
                    if(i.MainConsultingId == el.MainConsultingId) 
                    {
                        return Ok(new { Result = lawyersMainConsultingsService.getAll().Where(a => a.LawyerId == model.LawyerId), AdditionalInfo = "The Data Is Not Saved As You Have Already Saved Data Fir The Same Consultations" });
                    }
                }
            }
            
            foreach (var i in model.LawyerConsultDetails)
            {
                TbLawyersMainConsultings oTbLawyersMainConsultings = new TbLawyersMainConsultings();

                oTbLawyersMainConsultings.LawyerId = model.LawyerId;
                oTbLawyersMainConsultings.LawyerUserName = user.UserName;
                oTbLawyersMainConsultings.ConsultingNo = user.ConsultingNo;
                oTbLawyersMainConsultings.DaysOfWork = user.DaysOfWork;
                oTbLawyersMainConsultings.HoursOfWork = user.HoursOfWork;
                oTbLawyersMainConsultings.EvaluationNoOfStatrs = user.EvaluationNoOfStatrs;
                oTbLawyersMainConsultings.EvaluationNumerical = user.EvaluationNumerical;
                oTbLawyersMainConsultings.YearsOfExperience = user.YearsOfExperience;
                oTbLawyersMainConsultings.DocumentA = user.DocumentA;
                oTbLawyersMainConsultings.DocumentB = user.DocumentB;
                oTbLawyersMainConsultings.DocumentC = user.DocumentC;
                oTbLawyersMainConsultings.DocumentD = user.DocumentD;
                oTbLawyersMainConsultings.IdentityDocument = user.IdentityDocument;
                oTbLawyersMainConsultings.ShortDescription = user.ShortDescription;
                oTbLawyersMainConsultings.OTP = user.OTP;
                oTbLawyersMainConsultings.Image = user.Image;
                oTbLawyersMainConsultings.FirstName = user.FirstName;
                oTbLawyersMainConsultings.FamilyName = user.FamilyName;
                oTbLawyersMainConsultings.UserType = user.UserType;
                oTbLawyersMainConsultings.Status = "غير مفعل";
                oTbLawyersMainConsultings.DeviceToken = user.DeviceToken;
                oTbLawyersMainConsultings.IsActivated = user.IsActivated;
                if(i.MainConsultingId !=null) 
                {
                    oTbLawyersMainConsultings.MainConsultingId = i.MainConsultingId;
                    var consult = mainConsultingService.getAll().Where(a => a.MainConsultingId == i.MainConsultingId).FirstOrDefault();
                    if(consult != null) 
                    {
                        oTbLawyersMainConsultings.MainConsultingTitle = consult.MainConsultingTitle;
                        oTbLawyersMainConsultings.MainConsultingImage = consult.MainConsultingImage;
                        if(int.Parse(i.Consulting30MinutesCost) > int.Parse(highestconsult30min)) 
                        {
                            return BadRequest("The Consulting 30 minute cost is higher than the valid one (500 SAR)");
                        } 
                        oTbLawyersMainConsultings.Consulting30MinutesCost = i.Consulting30MinutesCost;
                        if (int.Parse(i.Consulting60MinutesCost) > int.Parse(highestconsult60min))
                        {
                            return BadRequest("The Consulting 60 minute cost is higher than the valid one (1000 SAR)");
                        }
                        oTbLawyersMainConsultings.Consulting60MinutesCost = i.Consulting60MinutesCost;
                        if (int.Parse(i.Consulting90MinutesCost) > int.Parse(highestconsult90min))
                        {
                            return BadRequest("The Consulting 90 minute cost is higher than the valid one (1500 SAR)");
                        }
                        oTbLawyersMainConsultings.Consulting90MinutesCost = i.Consulting90MinutesCost;
                    }
                    
                }
               

                var result = lawyersMainConsultingsService.Add(oTbLawyersMainConsultings);

                if (result == false)
                {
                    return Unauthorized();

                }
               
                   
               


            }
            return Ok(new { Result = lawyersMainConsultingsService.getAll().Where(a => a.LawyerId == model.LawyerId), AdditionalInfo = "The Data Is Saved" });


        }

        // PUT api/<LawyerMainConsultingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerMainConsultingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
