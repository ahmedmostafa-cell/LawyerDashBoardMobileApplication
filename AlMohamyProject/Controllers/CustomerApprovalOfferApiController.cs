using AlMohamyProject.Dtos;
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
    public class CustomerApprovalOfferApiController : ControllerBase
    {
        LawyerAppintmentsService lawyerAppintmentsService;
        ConsultingEstablishService consultingEstablishService;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferService offerService;
        AlMohamyDbContext ctx;
        EvaluationService evaluationService;
        public CustomerApprovalOfferApiController(LawyerAppintmentsService LawyerAppintmentsService, ConsultingEstablishService ConsultingEstablishService,EvaluationService EvaluationService, UserManager<ApplicationUser> userManager, OfferService OfferService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            offerService = OfferService;
            ctx = context;
            _userManager = userManager;
            evaluationService = EvaluationService;
            lawyerAppintmentsService = LawyerAppintmentsService;
        }
        // GET: api/<CustomerApprovalOfferApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CustomerApprovalOfferApiController>/5
        [HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    TbOffer item = offerService.getAll().Where(a=> a.OfferId == id).FirstOrDefault();
        //    item.OfferStatus = "مقبول";
        //    var resultOffer = offerService.Edit(item);
        //    TbConsultingEstablish element = consultingEstablishService.getAll().Where(a => a.ConsultingId == item.ConsultingId).FirstOrDefault();
        //    element.RequestStatus = "حالية";  //تم اضافة هذا السطر مؤتا لحين تفعيل الدفع
        //    element.LawyerId = item.LawyerId; 
        //    element.LawyerName = item.LawyerName;
        //    element.LawyerImage = item.LawyerImage;
        //    var resultConsulting = consultingEstablishService.Edit(element);
        //    if(resultOffer == true && resultConsulting == true) 
        //    {
        //        return Ok("The Data Is Saved");
        //    }
        //    else 
        //    {
        //        return BadRequest("The Data Is Not Saved");
        //    }
           
        //}

        // POST api/<CustomerApprovalOfferApiController>
        [HttpPost]
        public ActionResult<IEnumerable<LawyerApprovalAppointmentCustomizedDtos>> Post([FromForm] CustomerApprovalOfferDtos elementApproval)
        {
            TbOffer item = offerService.getAll().Where(a => a.OfferId == elementApproval.id).FirstOrDefault();
            item.OfferStatus = "مقبول";
            var resultOffer = offerService.Edit(item);
            TbConsultingEstablish element = consultingEstablishService.getAll().Where(a => a.ConsultingId == item.ConsultingId).FirstOrDefault();
            
            element.LawyerId = item.LawyerId;
            element.LawyerName = item.LawyerName;
            element.LawyerImage = item.LawyerImage;
            element.LawyerFamilyName = item.LawyerFamilyName;
            element.ConsultingDateAndTime = elementApproval.ConsultingDateAndTime;
            element.OrderStatus = "تمت الموافقة عل عرض محامي";
            element.ConsultingPeriodCost = item.CreatedBy;
            var resultConsulting = consultingEstablishService.Edit(element);
            if (resultOffer == true && resultConsulting == true)
            {
                List<TbLawyerAppintments> lstLawyerAppintments = lawyerAppintmentsService.getAll().Where(a => a.LawyerId == item.LawyerId).ToList();
                List<LawyerApprovalAppointmentCustomizedDtos> lstLawyerApprovalAppointmentCustomizedDtos = new List<LawyerApprovalAppointmentCustomizedDtos>();
                LawyerApprovalAppointmentCustomizedDtos elementDtos = new LawyerApprovalAppointmentCustomizedDtos();
                foreach (var i in lstLawyerAppintments) 
                {
                    elementDtos.ConsultingTypeId = element.ConsultingTypeId;
                    elementDtos.LawyerId = i.LawyerId;
                    elementDtos.ToHour = i.ToHour;
                    elementDtos.FromHour = i.FromHour;
                    elementDtos.CurrentState = i.CurrentState;
                    elementDtos.UpdatedDate = i.UpdatedDate;
                    elementDtos.CreatedDate = i.CreatedDate;
                    elementDtos.UpdatedDate = i.UpdatedDate;
                    elementDtos.WeekDay = i.WeekDay;
                    elementDtos.UpdatedBy = i.UpdatedBy;
                    elementDtos.CreatedBy = i.CreatedBy;
                    elementDtos.LawyerAppointmentId = i.LawyerAppointmentId;
                    elementDtos.LawyerName = i.LawyerName;
                    elementDtos.MainConsultingId = element.MainConsultingId;
                    elementDtos.MorEveFrst = i.MorEveFrst;
                    elementDtos.MorEveScond = i.MorEveScond;
                    elementDtos.Notes = i.Notes;

                    lstLawyerApprovalAppointmentCustomizedDtos.Add(elementDtos);

                }
                return Ok(lstLawyerApprovalAppointmentCustomizedDtos);
            }
            else
            {
                return BadRequest("The Data Is Not Saved");
            }
        }

        // PUT api/<CustomerApprovalOfferApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerApprovalOfferApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
