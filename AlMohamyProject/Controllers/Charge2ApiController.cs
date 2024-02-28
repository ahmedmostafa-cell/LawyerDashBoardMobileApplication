using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Charge2ApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ConsultingEstablishService consultingEstablishService;
        ChargeService _chargeService;
        AlMohamyDbContext ctx;
        public Charge2ApiController(ChargeService chargeService, AlMohamyDbContext context, ConsultingEstablishService ConsultingEstablishService, UserManager<ApplicationUser> UserManager)
        {
            _chargeService = chargeService;
            ctx = context;
            consultingEstablishService = ConsultingEstablishService;
            userManager = UserManager;
        }
        // GET: api/<Charge2ApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Charge2ApiController>/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {


            var TotalSalesPerEmployee = ctx.VwChargeDeducts.Where(a => a.ChargeTypeSender == "عملية شحن").ToList();
            List<ChargeGateDeductDtos> lst = new List<ChargeGateDeductDtos>();

            foreach (var i in TotalSalesPerEmployee)
            {
                ChargeGateDeductDtos oChargeGateDeductDtos = new ChargeGateDeductDtos();
                oChargeGateDeductDtos.PaymentGatesId = Guid.Parse(i.CreatedBy);
                oChargeGateDeductDtos.Percent = ctx.TbPaymentGatess.Where(a => a.PaymentGatesId == oChargeGateDeductDtos.PaymentGatesId).FirstOrDefault().CreatedBy;
                oChargeGateDeductDtos.TotalCharges = i.totalCharges;
                lst.Add(oChargeGateDeductDtos);
            }
            decimal totalDeductionOnCharge = 0;
            foreach (var i in lst)
            {
                totalDeductionOnCharge += ((i.TotalCharges * decimal.Parse(i.Percent)) / 100);
            }
            decimal totalPercentDeduct = totalDeductionOnCharge / lst.Sum(a => a.TotalCharges) * 100;
            decimal valuePaid = ctx.VwChargeDeducts.Where(a => a.ChargeTypeSender == "عملية دفع").Where(a => a.IdReciever == id.ToString()).Sum(a => a.totalCharges);
            decimal valueDeducted = (valuePaid * totalPercentDeduct) / 100;
            string deduct = valueDeducted.ToString();
            return deduct;
        }


        [HttpGet("{id}/{optionDate1}/{optionDate2}")]
        public string Get(Guid id, string optionDate1, string optionDate2)
        {
            var TotalSalesPerEmployee = ctx.VwChargeDeducts.Where(a => a.ChargeTypeSender == "عملية شحن").ToList();
            List<ChargeGateDeductDtos> lst = new List<ChargeGateDeductDtos>();

            foreach (var i in TotalSalesPerEmployee)
            {
                ChargeGateDeductDtos oChargeGateDeductDtos = new ChargeGateDeductDtos();
                oChargeGateDeductDtos.PaymentGatesId = Guid.Parse(i.CreatedBy);
                oChargeGateDeductDtos.Percent = ctx.TbPaymentGatess.Where(a => a.PaymentGatesId == oChargeGateDeductDtos.PaymentGatesId).FirstOrDefault().CreatedBy;
                oChargeGateDeductDtos.TotalCharges = i.totalCharges;
                lst.Add(oChargeGateDeductDtos);
            }
            decimal totalDeductionOnCharge = 0;
            foreach (var i in lst)
            {
                totalDeductionOnCharge += ((i.TotalCharges * decimal.Parse(i.Percent)) / 100);
            }


            List<VwChargeDeduct> lstLogHistories = ctx.VwChargeDeducts.Where(a => a.ChargeTypeSender == "عملية دفع").Where(a => a.IdReciever == id.ToString()).ToList();
            if (optionDate2 == null && optionDate1 == null)
            {
                lstLogHistories = lstLogHistories.ToList();
            }
            else if (optionDate2 != null && optionDate1 == null)
            {
                lstLogHistories = lstLogHistories.Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (optionDate2 != null && optionDate1 != null)
            {
                lstLogHistories = lstLogHistories.Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = lstLogHistories.Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }




            decimal totalPercentDeduct = totalDeductionOnCharge / lst.Sum(a => a.TotalCharges) * 100;
            decimal valuePaid = lstLogHistories.Sum(a => a.totalCharges);
            
            decimal valueDeducted = (valuePaid * totalPercentDeduct) / 100;
            string deduct = valueDeducted.ToString();
            return deduct;
        }

        // POST api/<Charge2ApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Charge2ApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Charge2ApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
