using AlMohamyProject.Dtos;
using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyFatoorahGetPaymentStatusWalletApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        private readonly ChargeService _chargeService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public MyFatoorahGetPaymentStatusWalletApiController(UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context, ChargeService chargeService)
        {
            consultingEstablishService = ConsultingEstablishService;
            Usermanager = usermanager;
            ctx = context;
            _chargeService = chargeService;
        }
        // GET: api/<MyFatoorahGetPaymentStatusWalletApiController>
        [HttpGet]
        public async Task<ActionResult<string>> Wallet(string id)
        {
            TbCharge oTbCharge = _chargeService.getAll().Where(a => a.ChargeId == Guid.Parse(id)).FirstOrDefault();
            string keyType = "invoiceid";
            var a = await MyFatoorahPaymentStatus.GetPaymentStatus(oTbCharge.MyfatoorahInvoiceId, keyType);
            PaymentStatus Trans_res = JsonConvert.DeserializeObject<PaymentStatus>(a);

            //TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(Trans_res.GatewayOrderRequest.Products[0].Description)).FirstOrDefault();
            if (Trans_res.Data.InvoiceStatus == "Paid")
            {
                oTbCharge.ChargeTypeSender = "عملية شحن";
                oTbCharge.ChargeValue = Trans_res.Data.InvoiceValue.ToString();
               
                oTbCharge.IdSender = Trans_res.Data.CustomerReference;
                oTbCharge.SenderName = Usermanager.Users.Where(a => a.Id == Trans_res.Data.CustomerReference).FirstOrDefault().FirstName;
                oTbCharge.CreatedBy = Trans_res.Data.UserDefinedField;
                var result = _chargeService.Edit(oTbCharge);
                if (result)
                {
                    return Ok("The Payment Is Done");
                }
                else
                {
                    return BadRequest("The Payment is not done");
                }


            }
            else
            {
                return BadRequest("The Payment is not done");

            }


        }

        // GET api/<MyFatoorahGetPaymentStatusWalletApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MyFatoorahGetPaymentStatusWalletApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MyFatoorahGetPaymentStatusWalletApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MyFatoorahGetPaymentStatusWalletApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
