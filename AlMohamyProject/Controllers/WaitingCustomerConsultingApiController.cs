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
    public class WaitingCustomerConsultingApiController : ControllerBase
    {
        OfferService offerService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public WaitingCustomerConsultingApiController(OfferService OfferService,ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            offerService = OfferService;

        }
        // GET: api/<WaitingCustomerConsultingApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WaitingCustomerConsultingApiController>/5
        [HttpGet("{id}")]
        //public ActionResult <IEnumerable<WaitingCustomerConsultingDtos>>  Get(Guid id)
        //{
        //    List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a=> a.RequestStatus == "بانتظار الرد" && a.ConsultingType == "طلب غير محدد").ToList();
        //    List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
        //    foreach(var item in lstTbConsultingEstablish) 
        //    {
        //        WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
        //        oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
        //        oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
        //        oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
        //        oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
        //        oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
        //        oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
        //        oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
        //        oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
        //        oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
        //        foreach(var element in offerService.getAll().Where(A=> A.ConsultingId == oWaitingCustomerConsultingDtos.ConsultingId)) 
        //        {
        //            oWaitingCustomerConsultingDtos.lstOffers.Add(element);


        //        }
        //        oWaitingCustomerConsultingDtos.NoOfOffers = oWaitingCustomerConsultingDtos.lstOffers.Count().ToString();
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

        // POST api/<WaitingCustomerConsultingApiController>
        [HttpPost]
        public ActionResult<IEnumerable<WaitingCustomerConsultingDtos>> Post([FromForm] Guid id)
        {
            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a => a.RequestStatus == "بانتظار الرد" && a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).ToList();
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in lstTbConsultingEstablish)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
                oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
                oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
                oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
                oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.ProposedCustomerPay = item.ProposedCustomerPay;
                oWaitingCustomerConsultingDtos.propsedTimeFinishConsult = item.propsedTimeFinishConsult;
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserFamilyName = item.UserFamilyName;
                oWaitingCustomerConsultingDtos.LawyerFamilyName = item.LawyerFamilyName;
                oWaitingCustomerConsultingDtos.LawyerName = item.LawyerName;
                oWaitingCustomerConsultingDtos.ConsultingDateAndTime = item.ConsultingDateAndTime;
                oWaitingCustomerConsultingDtos.LawyerId = item.LawyerId;
                oWaitingCustomerConsultingDtos.OrderStatus = item.OrderStatus;
                foreach (var element in offerService.getAll().Where(A => A.ConsultingId == oWaitingCustomerConsultingDtos.ConsultingId))
                {
                    oWaitingCustomerConsultingDtos.lstOffers.Add(element);


                }
                oWaitingCustomerConsultingDtos.NoOfOffers = oWaitingCustomerConsultingDtos.lstOffers.Count().ToString();
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

        // PUT api/<WaitingCustomerConsultingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WaitingCustomerConsultingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
