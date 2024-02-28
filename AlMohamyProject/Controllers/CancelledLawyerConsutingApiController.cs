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
    public class CancelledLawyerConsutingApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        OfferService offerService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public CancelledLawyerConsutingApiController(IAccountRepository accountRepository, OfferService OfferService, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            offerService = OfferService;
            _accountRepository = accountRepository;

        }
        // GET: api/<CancelledLawyerConsutingApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CancelledLawyerConsutingApiController>/5
        [HttpGet("{id}")]
        //public ActionResult<IEnumerable<WaitingCustomerConsultingDtos>> Get(Guid id)
        //{

        //    List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).Where(a => a.RequestStatus == "ملغية").ToList();
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

        // POST api/<CancelledLawyerConsutingApiController>
        [HttpPost]
        public ActionResult<IEnumerable<WaitingCustomerConsultingDtos>> Post([FromForm] Guid id)
        {
            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.LawyerId == id.ToString()).Where(a => a.RequestStatus == "ملغية").ToList();
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in lstTbConsultingEstablish)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();

                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.LawyerId = item.LawyerId;
                oWaitingCustomerConsultingDtos.LawyerName = item.LawyerName;
                oWaitingCustomerConsultingDtos.LawyerFamilyName = item.LawyerFamilyName;
                oWaitingCustomerConsultingDtos.LawyerImage = item.LawyerImage;
                oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
                oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
                oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
                oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
                oWaitingCustomerConsultingDtos.ConsultingDateAndTime = item.ConsultingDateAndTime;
                oWaitingCustomerConsultingDtos.OrderStatus = item.OrderStatus;
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
            }
            if (lstWaitingCustomerConsultingDtos != null)
            {
                return Ok(lstWaitingCustomerConsultingDtos);
            }
            else
            {
                return BadRequest(lstWaitingCustomerConsultingDtos);
            }
        }

        // PUT api/<CancelledLawyerConsutingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CancelledLawyerConsutingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
