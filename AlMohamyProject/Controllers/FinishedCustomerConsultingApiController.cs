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
    public class FinishedCustomerConsultingApiController : ControllerBase
    {
        OfferService offerService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public FinishedCustomerConsultingApiController(OfferService OfferService, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            offerService = OfferService;

        }
        // GET: api/<FinishedCustomerConsultingApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FinishedCustomerConsultingApiController>/5
        [HttpGet("{id}")]
        //public ActionResult<IEnumerable<WaitingCustomerConsultingDtos>> Get(Guid id)
        //{
        //    List<TbConsultingEstablish> lstTbConsultingEstablishCheck = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a => a.RequestStatus == "حالية").Where(a=> a.CreatedDate < DateTime.Now).ToList();
        //    foreach(var i in lstTbConsultingEstablishCheck) 
        //    {
        //        i.RequestStatus = "منتهية";
        //        consultingEstablishService.Edit(i);
        //    }
        //    List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a => a.RequestStatus == "منتهية").ToList();
        //    List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
        //    foreach (var item in lstTbConsultingEstablish)
        //    {
        //        WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();

        //        oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
        //        oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
        //        oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
        //        oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
        //        oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
        //        oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
        //        oWaitingCustomerConsultingDtos.LawyerId = item.LawyerId;
        //        oWaitingCustomerConsultingDtos.LawyerName = item.LawyerName;
        //        oWaitingCustomerConsultingDtos.LawyerImage = item.LawyerImage;
        //        oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
        //        oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
        //        oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
        //        oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;


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

        // POST api/<FinishedCustomerConsultingApiController>
        [HttpPost]
        public ActionResult<IEnumerable<WaitingCustomerConsultingDtos>> Post([FromForm] Guid id)
        {
            List<TbConsultingEstablish> lstTbConsultingEstablishCheck = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a => a.RequestStatus == "حالية" && a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).Where(a => DateTime.Parse(a.ConsultingDateAndTime) < DateTime.Now).ToList();
            foreach (var i in lstTbConsultingEstablishCheck)
            {
                i.RequestStatus = "منتهية";
                consultingEstablishService.Edit(i);
            }
            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.UserId == id.ToString()).Where(a => a.RequestStatus == "منتهية" && a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).ToList();
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in lstTbConsultingEstablish)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();

                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
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
                oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.ProposedCustomerPay = item.ProposedCustomerPay;
                oWaitingCustomerConsultingDtos.MainConsultingId = item.MainConsultingId;
                oWaitingCustomerConsultingDtos.ConsultingTypeId = item.ConsultingTypeId;
                oWaitingCustomerConsultingDtos.UpdatedDate = item.UpdatedDate;
                oWaitingCustomerConsultingDtos.ConsultingTypeId = item.ConsultingTypeId;
                oWaitingCustomerConsultingDtos.MainConsultingId = item.MainConsultingId;
                oWaitingCustomerConsultingDtos.ConsultingDateAndTime = item.ConsultingDateAndTime;
                oWaitingCustomerConsultingDtos.OrderStatus = item.OrderStatus;
                //oWaitingCustomerConsultingDtos.
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

        // PUT api/<FinishedCustomerConsultingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FinishedCustomerConsultingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
