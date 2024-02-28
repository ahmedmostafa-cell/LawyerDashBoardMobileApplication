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
    public class IncompleteConsultationApiController : ControllerBase
    {
        ConsultingEstablishService consultingEstablishService;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferService offerService;
        AlMohamyDbContext ctx;
        public IncompleteConsultationApiController(ConsultingEstablishService ConsultingEstablishService, UserManager<ApplicationUser> userManager, OfferService OfferService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            offerService = OfferService;
            ctx = context;
            _userManager = userManager;
        }
        // GET: api/<IncompleteConsultationApiController>
        [HttpGet]
        public IEnumerable<WaitingCustomerConsultingDtos> Get()
        {
            List<TbConsultingEstablish> listConsultingEstablishReport = new List<TbConsultingEstablish>();
            List<TbConsultingEstablish> listConsultingEstablish = consultingEstablishService.getAll().Where(a=> a.RequestStatus == "بانتظار الرد").ToList();
            List<TbOffer> listOffer = offerService.getAll().Where(a=> a.OfferStatus == "بانتظار الرد").ToList();
            foreach(var consult in listConsultingEstablish) 
            {
                foreach(var offer in listOffer) 
                {
                    if(consult.ConsultingId == offer.ConsultingId) 
                    {
                        listConsultingEstablishReport.Add(consult);

                    }
                }
            }
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in listConsultingEstablishReport)
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
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserPhone = _userManager.Users.Where(a => a.Id == item.UserId).FirstOrDefault().UserName;
                foreach (var element in offerService.getAll().Where(A => A.ConsultingId == oWaitingCustomerConsultingDtos.ConsultingId))
                {
                    oWaitingCustomerConsultingDtos.lstOffers.Add(element);


                }
                oWaitingCustomerConsultingDtos.NoOfOffers = oWaitingCustomerConsultingDtos.lstOffers.Count().ToString();
                lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
            }
           
                return lstWaitingCustomerConsultingDtos;
            }
          
           
        

        // GET api/<IncompleteConsultationApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<IncompleteConsultationApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<IncompleteConsultationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IncompleteConsultationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
