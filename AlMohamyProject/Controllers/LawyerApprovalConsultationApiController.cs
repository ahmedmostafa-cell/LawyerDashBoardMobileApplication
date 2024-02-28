using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerApprovalConsultationApiController : ControllerBase
    {
        ConsultingEstablishService consultingEstablishService;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferService offerService;
        AlMohamyDbContext ctx;
        EvaluationService evaluationService;
        public LawyerApprovalConsultationApiController(ConsultingEstablishService ConsultingEstablishService, EvaluationService EvaluationService, UserManager<ApplicationUser> userManager, OfferService OfferService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            offerService = OfferService;
            ctx = context;
            _userManager = userManager;
            evaluationService = EvaluationService;
        }
        // GET: api/<LawyerApprovalConsultationApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerApprovalConsultationApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LawyerApprovalConsultationApiController>
        [HttpPost("approveConsult")]
        public IActionResult Post([FromForm] LawyerApprovalDtos model)
        {
            TbConsultingEstablish element = consultingEstablishService.getAll().Where(a => a.ConsultingId == model.ConsultingId).FirstOrDefault();
            element.OrderStatus = "تم الموافقة من المحامي";
            element.LawyerId = model.LawyerId;
            element.LawyerName = _userManager.Users.Where(a => a.Id == model.LawyerId).FirstOrDefault().FirstName;
            element.LawyerImage = _userManager.Users.Where(a => a.Id == model.LawyerId).FirstOrDefault().Image;
            var resultConsulting = consultingEstablishService.Edit2(element);
            if (resultConsulting.ConsultingId != null)
            {

                return Ok(new { Result = resultConsulting, AdditionalInfo = "The Data Is Saved" });
            }
            else
            {
                return BadRequest("The Data Is Not Saved");
            }
        }

        // PUT api/<LawyerApprovalConsultationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerApprovalConsultationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
