using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentCustomerConsultingApiController : ControllerBase
    {

        OfferService offerService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public CurrentCustomerConsultingApiController(OfferService OfferService, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            offerService = OfferService;

        }
        // GET: api/<CurrentCustomerConsultingApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CurrentCustomerConsultingApiController>/5
        [HttpGet("{id}")]
        //public ActionResult<IEnumerable<WaitingCustomerConsultingDtos>> Get(Guid id)
        //{
        //    List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a=> a.RequestStatus == "حالية").ToList();
        //    List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
        //    foreach (var item in lstTbConsultingEstablish)
        //    {
        //        WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
               
        //        oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
        //        oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
        //        oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
        //        oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
        //        oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
        //        oWaitingCustomerConsultingDtos.LawyerId = item.LawyerId;
        //        oWaitingCustomerConsultingDtos.LawyerName = item.LawyerName;
        //        oWaitingCustomerConsultingDtos.LawyerImage = item.LawyerImage;
        //        oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
        //        oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
        //        oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
        //        oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
        //        TimeSpan diff = DateTime.Now - DateTime.Parse(item.ConsultingDateAndTime);
        //        oWaitingCustomerConsultingDtos.TimeRemainingForConsultingToStart = diff.ToString();


        //        lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
        //    }
        //    if (lstWaitingCustomerConsultingDtos != null)
        //    {
        //        return Ok(lstWaitingCustomerConsultingDtos);
        //    }
        //    else
        //    {
        //        return BadRequest("There is No Data");
        //    }
        //}

        // POST api/<CurrentCustomerConsultingApiController>
        [HttpPost]
        public ActionResult<IEnumerable<WaitingCustomerConsultingDtos>> Post([FromForm] Guid id)
        {
            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a => a.RequestStatus == "حالية"&&a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).ToList();
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in lstTbConsultingEstablish)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();

                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserFamilyName = item.UserFamilyName;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.LawyerId = item.LawyerId;
                oWaitingCustomerConsultingDtos.LawyerName = item.LawyerName;
                oWaitingCustomerConsultingDtos.LawyerFamilyName = item.LawyerFamilyName;
                oWaitingCustomerConsultingDtos.LawyerImage = item.LawyerImage;
                oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
                oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
                oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
                oWaitingCustomerConsultingDtos.ProposedCustomerPay = item.ProposedCustomerPay;
                oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
                oWaitingCustomerConsultingDtos.ConsultingDateAndTime = item.ConsultingDateAndTime;
                oWaitingCustomerConsultingDtos.OrderStatus = item.OrderStatus;
                if (item.ConsultingPeriod == "ثلاثون دقيقة") 
                {
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsult = DateTime.Parse(item.ConsultingDateAndTime).AddMinutes(30);
                    string inputDate = oWaitingCustomerConsultingDtos.propsedTimeFinishConsult.ToString();

                    // Parse the input date string into a DateTime object
                    DateTime dateTime = DateTime.Parse(inputDate);

                    // Convert the DateTime object to the desired format
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsultFormatted =  dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                  
                }
                else if (item.ConsultingPeriod == "ستون دقيقة") 
                {
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsult = DateTime.Parse(item.ConsultingDateAndTime).AddMinutes(60);
                    string inputDate = oWaitingCustomerConsultingDtos.propsedTimeFinishConsult.ToString();

                    // Parse the input date string into a DateTime object
                    DateTime dateTime = DateTime.Parse(inputDate);

                    // Convert the DateTime object to the desired format
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsultFormatted = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                }
                else if (item.ConsultingPeriod == "تسعون دقيقة") 
                {
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsult = DateTime.Parse(item.ConsultingDateAndTime).AddMinutes(90);
                    string inputDate = oWaitingCustomerConsultingDtos.propsedTimeFinishConsult.ToString();

                    // Parse the input date string into a DateTime object
                    DateTime dateTime = DateTime.Parse(inputDate);

                    // Convert the DateTime object to the desired format
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsultFormatted = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                }

                TimeSpan diff = DateTime.Now - DateTime.Parse(item.ConsultingDateAndTime);
                oWaitingCustomerConsultingDtos.TimeRemainingForConsultingToStart = diff.ToString();
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.ProposedCustomerPay = item.ProposedCustomerPay;
                lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
            }
            if (lstWaitingCustomerConsultingDtos.Count != 0)
            {
                return Ok(lstWaitingCustomerConsultingDtos);
            }
            else
            {
                return BadRequest(lstWaitingCustomerConsultingDtos);
            }
        }

        // PUT api/<CurrentCustomerConsultingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CurrentCustomerConsultingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
