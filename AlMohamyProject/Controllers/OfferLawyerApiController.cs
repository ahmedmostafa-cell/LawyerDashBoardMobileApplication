using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferLawyerApiController : ControllerBase
    {
        ConsultingEstablishService consultingEstablishService;
        MainConsultingService mainConsultingService;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferService offerService;
        AlMohamyDbContext ctx;
        EvaluationService evaluationService;
        public OfferLawyerApiController(ConsultingEstablishService ConsultingEstablishService,MainConsultingService MainConsultingService,EvaluationService EvaluationService, UserManager<ApplicationUser> userManager, OfferService OfferService,AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            mainConsultingService = MainConsultingService;
            offerService = OfferService;
             ctx = context;
            _userManager = userManager;
            evaluationService = EvaluationService;
        }
        // GET: api/<OfferLawyerApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OfferLawyerApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OfferLawyerApiController>
        [HttpPost("offerService")]
        public IActionResult Post([FromForm] ServicesOfferViewPageModel services)
        {
            string serviceName = consultingEstablishService.getAll().Where(a=> a.ConsultingId == services.ConsultingId).FirstOrDefault().ServiceName;
            bool resultt = true;
            foreach (var i in ctx.TbOffers)
            {
                if (i.LawyerId == services.LawyerId && (i.ConsultingId == services.ConsultingId))
                {
                    resultt = false;
                    break;
                }




            }


            if (!resultt)
            {
                return BadRequest("لقد تم ارسال عرض سعر من قبل");

            }
            TbOffer oTbOffer = new TbOffer();
            oTbOffer.CreatedBy = services.CreatedBy;
            oTbOffer.ConsultingId = services.ConsultingId;
            oTbOffer.UserId = services.UserId;
            oTbOffer.LawyerId = services.LawyerId;
            oTbOffer.UserName = _userManager.Users.Where(a => a.Id == oTbOffer.UserId).FirstOrDefault().FirstName;
            oTbOffer.LawyerName = _userManager.Users.Where(a => a.Id == oTbOffer.LawyerId).FirstOrDefault().FirstName;
            oTbOffer.LawyerImage = _userManager.Users.Where(a => a.Id == oTbOffer.LawyerId).FirstOrDefault().Image;
            oTbOffer.Notes = serviceName;
            oTbOffer.LawyerFamilyName = _userManager.Users.Where(a => a.Id == oTbOffer.LawyerId).FirstOrDefault().FamilyName;

            oTbOffer.LawyerShortDescription = _userManager.Users.Where(a => a.Id == oTbOffer.LawyerId).FirstOrDefault().ShortDescription;
            oTbOffer.LawyersExperinceYears = _userManager.Users.Where(a => a.Id == oTbOffer.LawyerId).FirstOrDefault().YearsOfExperience;
            DateTime date = DateTime.Now.AddMinutes(int.Parse(ctx.TbSettings.First().OffersValidityDays));
            oTbOffer.OfferEndDate = date.ToString();
            oTbOffer.OfferStatus = "بانتظار الرد";
            var result = offerService.Add(oTbOffer);
            return Ok(result);
        }


        // PUT api/<OfferLawyerApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OfferLawyerApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
