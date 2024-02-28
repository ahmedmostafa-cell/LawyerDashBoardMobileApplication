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
    public class PayCosultingDataApiController : ControllerBase
    {
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public PayCosultingDataApiController(ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;

        }
        // GET: api/<PayCosultingDataApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PayCosultingDataApiController>/5
        [HttpGet("{id}")]
        //public ActionResult PayConsultingDtos (string id)
        //{
        //    TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(id)).FirstOrDefault();
        //    int vat = int.Parse(ctx.TbSettings.FirstOrDefault().ValueAddedTax);
        //    if(oTbConsultingEstablish.ServiceId == Guid.Parse("d6902f37-784d-4d2a-ae16-cdf8666d0adf")) 
        //    {
        //        int TheTotal = int.Parse(oTbConsultingEstablish.DelegationValueApproved) + vat;
        //        oTbConsultingEstablish.TheTotalDelegation = TheTotal.ToString();
        //        oTbConsultingEstablish.PromocodeDiscountValue = "";
        //        oTbConsultingEstablish.ConsultingVatvalue = vat.ToString();
        //        consultingEstablishService.Edit(oTbConsultingEstablish);
        //        PayConsultingDtos oPayConsultingDtos = new PayConsultingDtos();
        //        oPayConsultingDtos.Cost = oTbConsultingEstablish.DelegationValueApproved;
        //        oPayConsultingDtos.CostAfterDiscount = oTbConsultingEstablish.PromocodeDiscountValue;
        //        oPayConsultingDtos.ConsultingVatvalue = oTbConsultingEstablish.ConsultingVatvalue;
        //        oPayConsultingDtos.TheTotal = oTbConsultingEstablish.TheTotalDelegation;
        //        if (oPayConsultingDtos != null)
        //        {
        //            return Ok(oPayConsultingDtos);
        //        }
        //        else
        //        {
        //            return BadRequest("There is No Data");
        //        }
        //    }
        //    else 
        //    {
        //        int TheTotal = int.Parse(oTbConsultingEstablish.ConsultingPeriodCost) + vat;
        //        oTbConsultingEstablish.TheTotal = TheTotal.ToString();
        //        oTbConsultingEstablish.PromocodeDiscountValue = "";
        //        oTbConsultingEstablish.ConsultingVatvalue = vat.ToString();
        //        consultingEstablishService.Edit(oTbConsultingEstablish);
        //        PayConsultingDtos oPayConsultingDtos = new PayConsultingDtos();
        //        oPayConsultingDtos.Cost = oTbConsultingEstablish.ConsultingPeriodCost;
        //        oPayConsultingDtos.CostAfterDiscount = oTbConsultingEstablish.PromocodeDiscountValue;
        //        oPayConsultingDtos.ConsultingVatvalue = oTbConsultingEstablish.ConsultingVatvalue;
        //        oPayConsultingDtos.TheTotal = oTbConsultingEstablish.TheTotal;
        //        if (oPayConsultingDtos != null)
        //        {
        //            return Ok(oPayConsultingDtos);
        //        }
        //        else
        //        {
        //            return BadRequest("There is No Data");
        //        }
        //    }
           

        //}

        // POST api/<PayCosultingDataApiController>
        [HttpPost]
        public ActionResult PayConsultingDtos([FromForm] string id)
        {
            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(id)).FirstOrDefault();
            int vat = int.Parse(ctx.TbSettings.FirstOrDefault().ValueAddedTax);
            if (oTbConsultingEstablish.ServiceId == Guid.Parse("d6902f37-784d-4d2a-ae16-cdf8666d0adf"))
            {
                int TheTotal = int.Parse(oTbConsultingEstablish.DelegationValueApproved) + (int.Parse(oTbConsultingEstablish.DelegationValueApproved)* vat )/100;
                oTbConsultingEstablish.TheTotalDelegation = TheTotal.ToString();
                oTbConsultingEstablish.PromocodeDiscountValue = "";
                oTbConsultingEstablish.ConsultingVatvalue = vat.ToString();
                consultingEstablishService.Edit(oTbConsultingEstablish);
                PayConsultingDtos oPayConsultingDtos = new PayConsultingDtos();
                oPayConsultingDtos.Cost = oTbConsultingEstablish.DelegationValueApproved;
                oPayConsultingDtos.CostAfterDiscount = oTbConsultingEstablish.PromocodeDiscountValue;
                oPayConsultingDtos.ConsultingVatvalue = oTbConsultingEstablish.ConsultingVatvalue;
                oPayConsultingDtos.TheTotal = oTbConsultingEstablish.TheTotalDelegation;
                if (oPayConsultingDtos != null)
                {
                    return Ok(oPayConsultingDtos);
                }
                else
                {
                    return BadRequest("There is No Data");
                }
            }
            else
            {
                int TheTotal = int.Parse(oTbConsultingEstablish.ConsultingPeriodCost) + (int.Parse(oTbConsultingEstablish.ConsultingPeriodCost) * vat) / 100;
				oTbConsultingEstablish.TheTotal = TheTotal.ToString();
                oTbConsultingEstablish.PromocodeDiscountValue = "";
                oTbConsultingEstablish.ConsultingVatvalue = vat.ToString();
                consultingEstablishService.Edit(oTbConsultingEstablish);
                PayConsultingDtos oPayConsultingDtos = new PayConsultingDtos();
                oPayConsultingDtos.Cost = oTbConsultingEstablish.ConsultingPeriodCost;
                oPayConsultingDtos.CostAfterDiscount = oTbConsultingEstablish.PromocodeDiscountValue;
                oPayConsultingDtos.ConsultingVatvalue = oTbConsultingEstablish.ConsultingVatvalue;
                oPayConsultingDtos.TheTotal = oTbConsultingEstablish.TheTotal;
                if (oPayConsultingDtos != null)
                {
                    return Ok(oPayConsultingDtos);
                }
                else
                {
                    return BadRequest("There is No Data");
                }
            }
        }

        // PUT api/<PayCosultingDataApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PayCosultingDataApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
