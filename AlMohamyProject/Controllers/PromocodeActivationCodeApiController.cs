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
    public class PromocodeActivationCodeApiController : ControllerBase
    {
        ConsultingEstablishService consultingEstablishService;
        PromocodeService promocodeService;
        AlMohamyDbContext ctx;
        public PromocodeActivationCodeApiController(ConsultingEstablishService ConsultingEstablishService, PromocodeService PromocodeService, AlMohamyDbContext context)
        {
            promocodeService = PromocodeService;
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;

        }
        // GET: api/<PromocodeActivationCodeApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PromocodeActivationCodeApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PromocodeActivationCodeApiController>
        [HttpPost("PromocodeActivate")]
        public ActionResult<PayConsultingDtos> Post([FromForm] PromocodeActivateDtos promo)
        {
            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(promo.ConsultingId)).FirstOrDefault();
            PayConsultingDtos oPayConsultingDtos = new PayConsultingDtos();
            int vat = int.Parse(ctx.TbSettings.FirstOrDefault().ValueAddedTax);
            TbPromocode oTbPromocode = promocodeService.getAll().Where(a => a.PromocodeTitle == promo.PromocodeTitle).FirstOrDefault();
            if(oTbPromocode!=null) 
            {
                if (oTbConsultingEstablish.ServiceId == Guid.Parse("d6902f37-784d-4d2a-ae16-cdf8666d0adf")) 
                {
                    int CostAfterPromocode = int.Parse(oTbConsultingEstablish.DelegationValueApproved) - ((int.Parse(oTbPromocode.PromocodeDiscountPercent) * int.Parse(oTbConsultingEstablish.DelegationValueApproved)) / 100);
                    oTbConsultingEstablish.DelegationPromocodeDiscountValue = CostAfterPromocode.ToString();
                    int TheTotal = CostAfterPromocode + vat;
                    oTbConsultingEstablish.TheTotalDelegation = TheTotal.ToString();
                    oTbConsultingEstablish.ConsultingVatvalue = vat.ToString();
                    consultingEstablishService.Edit(oTbConsultingEstablish);
                    oPayConsultingDtos.Cost = oTbConsultingEstablish.DelegationValueApproved;
                    oPayConsultingDtos.CostAfterDiscount = oTbConsultingEstablish.DelegationPromocodeDiscountValue;
                    oPayConsultingDtos.ConsultingVatvalue = oTbConsultingEstablish.ConsultingVatvalue;
                    oPayConsultingDtos.TheTotal = oTbConsultingEstablish.TheTotalDelegation;
                }
                else 
                {
                    int CostAfterPromocode = int.Parse(oTbConsultingEstablish.ConsultingPeriodCost) - ((int.Parse(oTbPromocode.PromocodeDiscountPercent) * int.Parse(oTbConsultingEstablish.ConsultingPeriodCost)) / 100);
                    oTbConsultingEstablish.PromocodeDiscountValue = CostAfterPromocode.ToString();
                    int TheTotal = CostAfterPromocode + vat;
                    oTbConsultingEstablish.TheTotal = TheTotal.ToString();
                    oTbConsultingEstablish.ConsultingVatvalue = vat.ToString();
                    consultingEstablishService.Edit(oTbConsultingEstablish);
                    oPayConsultingDtos.Cost = oTbConsultingEstablish.ConsultingPeriodCost;
                    oPayConsultingDtos.CostAfterDiscount = oTbConsultingEstablish.PromocodeDiscountValue;
                    oPayConsultingDtos.ConsultingVatvalue = oTbConsultingEstablish.ConsultingVatvalue;
                    oPayConsultingDtos.TheTotal = oTbConsultingEstablish.TheTotal;

                }

               
            }
            else 
            {
               
                int TheTotal = int.Parse(oTbConsultingEstablish.ConsultingPeriodCost) + vat;
                oTbConsultingEstablish.TheTotal = TheTotal.ToString();
                oTbConsultingEstablish.PromocodeDiscountValue = "";
                oTbConsultingEstablish.ConsultingVatvalue = vat.ToString();
                consultingEstablishService.Edit(oTbConsultingEstablish);
                oPayConsultingDtos.Cost = oTbConsultingEstablish.ConsultingPeriodCost;
                oPayConsultingDtos.CostAfterDiscount = oTbConsultingEstablish.PromocodeDiscountValue;
                oPayConsultingDtos.ConsultingVatvalue = oTbConsultingEstablish.ConsultingVatvalue;
                oPayConsultingDtos.TheTotal = oTbConsultingEstablish.TheTotal;
            }
           
            if (oPayConsultingDtos != null)
            {
                return Ok(oPayConsultingDtos);
            }
            else
            {
                return BadRequest("There is No Data");
            }
        }

        // PUT api/<PromocodeActivationCodeApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PromocodeActivationCodeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
