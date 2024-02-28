using AlMohamyProject.Dtos;
using AlMohamyProject.Helpers;
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
    public class customeObject
    {
        public string result { get; set; }

        public IEnumerable<OfferCustomerViewPageModel> lstOfferCustomers { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class OfferCustomerApiController : ControllerBase
    {
        private readonly ConsultingEstablishService consultingEstablishService;
        EvaluationService evaluationService;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferService offerService;
        AlMohamyDbContext ctx;
        public OfferCustomerApiController(EvaluationService EvaluationService, UserManager<ApplicationUser> userManager, OfferService OfferService, AlMohamyDbContext context, ConsultingEstablishService ConsultingEstablishService)
        {
            evaluationService = EvaluationService;
            offerService = OfferService;
            ctx = context;
            _userManager = userManager;
            consultingEstablishService = ConsultingEstablishService;
        }
        // GET: api/<OfferCustomerApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OfferCustomerApiController>/5
        [HttpGet("{id}")]
        //public ActionResult<IEnumerable<OfferCustomerViewPageModel>> Get(string id)
        //{
        //    List<TbOffer> lstOffers = offerService.getAll().Where(a => a.UserId == id).Where(a => a.OfferStatus == "بانتظار الرد").ToList();

        //    List<OfferCustomerViewPageModel> list = new List<OfferCustomerViewPageModel>();
        //    List<TbEvaluation> lstEvaluations = evaluationService.getAll();
        //    //list<TbEvaluation> lstEvaluations = evaluationService.getAll().Where(a=> a.ToBeEvaluatedId == element.LawyerId)
        //    if (lstOffers.Count == 0)
        //    {
        //        return BadRequest("There Is No Offers");

        //    }
        //    foreach (var i in lstOffers)
        //    {
        //        OfferCustomerViewPageModel element = new OfferCustomerViewPageModel();
        //        element.OfferId = i.OfferId;
        //        element.ConsultingId = i.ConsultingId;
        //        element.LawyerId = i.LawyerId;
        //        element.LawyerName = i.LawyerName;
        //        element.LawyerImage = _userManager.Users.Where(a => a.Id == i.LawyerId).FirstOrDefault().Image;
        //        double noOfStars = lstEvaluations.Where(a => a.ToBeEvaluatedId == element.LawyerId).Sum(a => int.Parse(a.StartsNo));
        //        if (noOfStars > 0)
        //        {
        //            double countIfEvaluation = lstEvaluations.Where(a => a.ToBeEvaluatedId == element.LawyerId).Count();
        //            double evaluation = noOfStars / countIfEvaluation;
        //            element.LawyerEvalNoStarts = (noOfStars / countIfEvaluation).ToString();
        //            element.LawyersEvalNumerical = evaluation.ToString();

        //        }
        //        else
        //        {
        //            element.LawyerEvalNoStarts = "No Evaluation";
        //            element.LawyersEvalNumerical = "No Evaluation";
        //        }

        //        element.CreatedDate = i.CreatedDate;
        //        element.LawyerShortDescription = i.LawyerShortDescription;
        //        element.LawyersExperinceYears = i.LawyersExperinceYears;
        //        element.OfferEndDate = i.OfferEndDate;
        //        TimeSpan? duration = DateTime.Parse(element.OfferEndDate) - DateTime.Now;
        //        int hours = (int)duration.Value.TotalHours;
        //        if (hours < 0)
        //        {
        //            continue;
        //        }
        //        TimeSpan diff = DateTime.Now - DateTime.Parse(element.OfferEndDate);
        //        element.CreatedBy = diff.ToString();
        //        element.UpdatedBy = hours.ToString();
        //        list.Add(element);
        //    };

        //    if (list.Count > 0)
        //    {
        //        customeObject objectt = new customeObject()
        //        {

        //            lstOfferCustomers = list,
        //            result = "There is Offers Sent To You",

        //        };
        //        return Ok(objectt);

        //    }
        //    else
        //    {
        //        customeObject objectt = new customeObject()
        //        {

        //            lstOfferCustomers = list,
        //            result = "There is Offers Sent To You But Expired",

        //        };
        //        return Ok(objectt);
        //    }




        //}

        // POST api/<OfferCustomerApiController>
        [HttpPost]
        public ActionResult<IEnumerable<OfferCustomerViewPageModel>> Post([FromForm] string id)
        {
            List<TbOffer> lstOffers = offerService.getAll().Where(a => a.ConsultingId == Guid.Parse(id)).Where(a => a.OfferStatus == "بانتظار الرد").ToList();
            TbConsultingEstablish elementconsult = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(id)).FirstOrDefault();
            List<OfferCustomerViewPageModel> list = new List<OfferCustomerViewPageModel>();
            List<TbEvaluation> lstEvaluations = evaluationService.getAll();
            //list<TbEvaluation> lstEvaluations = evaluationService.getAll().Where(a=> a.ToBeEvaluatedId == element.LawyerId)
            if (lstOffers.Count == 0)
            {
                return BadRequest("There Is No Offers");

            }
            foreach (var i in lstOffers)
            {
                OfferCustomerViewPageModel element = new OfferCustomerViewPageModel();
                element.OfferId = i.OfferId;
                element.ConsultingId = i.ConsultingId;
                element.LawyerId = i.LawyerId;
                element.OfferValue = i.CreatedBy;
                element.ServiceId = elementconsult.ServiceId;
                element.LawyerName = i.LawyerName;
                element.ConsultingTypeId = elementconsult.ConsultingTypeId;
                element.MainConsultingId = elementconsult.MainConsultingId;
                element.LawyerImage = _userManager.Users.Where(a => a.Id == i.LawyerId).FirstOrDefault().Image;
                double noOfStars = lstEvaluations.Where(a => a.ToBeEvaluatedId == element.LawyerId).Sum(a => double.Parse(a.StartsNo));
                if (noOfStars > 0)
                {
                    double countIfEvaluation = lstEvaluations.Where(a => a.ToBeEvaluatedId == element.LawyerId).Count();
                    double evaluation = noOfStars / countIfEvaluation;
                    element.LawyerEvalNoStarts = (noOfStars / countIfEvaluation).ToString();
                    element.LawyersEvalNumerical = evaluation.ToString();

                }
                else
                {
                    element.LawyerEvalNoStarts = "No Evaluation";
                    element.LawyersEvalNumerical = "No Evaluation";
                }

                element.CreatedDate = i.CreatedDate;
                element.LawyerShortDescription = i.LawyerShortDescription;
                element.LawyersExperinceYears = i.LawyersExperinceYears;
                element.OfferEndDate = i.OfferEndDate;
                TimeSpan? duration = DateTime.Parse(element.OfferEndDate) - DateTime.Now;
                int minutes = (int)duration.Value.TotalMinutes;
                if (minutes < 0)
                {
                    continue;
                }
                TimeSpan diff = DateTime.Now - DateTime.Parse(element.OfferEndDate);
                element.CreatedBy = diff.ToString();
                element.UpdatedBy = minutes.ToString();
                list.Add(element);
            };

            if (list.Count > 0)
            {
                customeObject objectt = new customeObject()
                {

                    lstOfferCustomers = list,
                    result = "There is Offers Sent To You",

                };
                return Ok(objectt);

            }
            else
            {
                customeObject objectt = new customeObject()
                {

                    lstOfferCustomers = list,
                    result = "There is Offers Sent To You But Expired",

                };
                return Ok(objectt);
            }
        }

        // PUT api/<OfferCustomerApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OfferCustomerApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
