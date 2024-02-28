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
    public class DetailsConsiltationApiController : ControllerBase
    {
        OfferService offerService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public DetailsConsiltationApiController(OfferService OfferService, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            offerService = OfferService;

        }
        // GET: api/<DetailsConsiltationApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DetailsConsiltationApiController>/5
        [HttpGet("{id}")]
        //public TbConsultingEstablish Get(Guid id)
        //{
        //    WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
        //    oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
        //    foreach (var element in offerService.getAll().Where(A => A.ConsultingId == id))
        //    {
        //        oWaitingCustomerConsultingDtos.lstOffers.Add(element);


        //    }
        //    oWaitingCustomerConsultingDtos.NoOfOffers = oWaitingCustomerConsultingDtos.lstOffers.Count().ToString();

        //    TbConsultingEstablish item = consultingEstablishService.getAll().Where(A => A.ConsultingId == id).FirstOrDefault();
        //    item.NoOfOffers = oWaitingCustomerConsultingDtos.NoOfOffers;
        //    return item;
        //}

        // POST api/<DetailsConsiltationApiController>
        [HttpPost]
        public TbConsultingEstablish Post([FromForm] Guid id)
        {
            WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
            oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
            foreach (var element in offerService.getAll().Where(A => A.ConsultingId == id))
            {
                oWaitingCustomerConsultingDtos.lstOffers.Add(element);


            }
            oWaitingCustomerConsultingDtos.NoOfOffers = oWaitingCustomerConsultingDtos.lstOffers.Count().ToString();

            TbConsultingEstablish item = consultingEstablishService.getAll().Where(A => A.ConsultingId == id).FirstOrDefault();
            item.NoOfOffers = oWaitingCustomerConsultingDtos.NoOfOffers;
            return item;
        }

        // PUT api/<DetailsConsiltationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DetailsConsiltationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
