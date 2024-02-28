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
    public class MyFatoorahLinkRecievePaymentApiController : ControllerBase
    {
     
        private readonly ChargeService _chargeService;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public MyFatoorahLinkRecievePaymentApiController(ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context, ChargeService chargeService)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            _chargeService = chargeService;
        }
        // GET: api/<MyFatoorahLinkRecievePaymentApiController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
           
            return new string[] { "value1", "value2" };
        }

        // GET api/<MyFatoorahLinkRecievePaymentApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MyFatoorahLinkRecievePaymentApiController>
        [HttpPost]
        public async Task<ActionResult<InvoiceResponse>> Post([FromForm] MyFatoorahLinkDtos model)
        {
            var consult = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(model.ConsultingId)).FirstOrDefault();
            //if (consult.OrderStatus != "تم الموافقة من المحامي" && consult.OrderStatus != "تمت الموافقة عل عرض محامي")
            //{
            //    return BadRequest("There is No Approval Either From Lawyer on The Consult Or From The User On The Offer");
            //}
            var a = await MyFatoorah.ExecutePayment(model.PaymentMethodId , model.invoiceValue , model.ConsultingId , model.PaymentGateId , model.UserFirstName , model.UserEmail);
            //var a = await MyFatoorah.ExecutePayment();
            //var executePaymentRequest = new
            //{
            //    //required fields
            //    PaymentMethodId = model.PaymentMethodId,
            //    InvoiceValue = int.Parse(model.invoiceValue),
            //    CallBackUrl = "https://example.com/callback",
            //    ErrorUrl = "https://example.com/error",
            //    //optional fields 
            //    CustomerName = "Customer Name",
            //    DisplayCurrencyIso = "KWD",
            //    MobileCountryCode = "965",
            //    CustomerMobile = "12345678",
            //    CustomerEmail = "email@example.com",
            //    Language = "En",
            //    CustomerReference = "",
            //    CustomerCivilId = "",
            //    UserDefinedField = "",
            //    ExpiryDate = DateTime.Now.AddYears(1),
            //    // to add suppliers
            //    Suppliers = new[] {
            //            new {
            //              SupplierCode = 1, InvoiceShare = 1000, ProposedShare = 500
            //            }
            //     }

            //};
            //var executeRequestJSON = JsonConvert.SerializeObject(executePaymentRequest);
            InvoiceResponse Trans_res = JsonConvert.DeserializeObject<InvoiceResponse>(a);
            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(model.ConsultingId)).FirstOrDefault();
            oTbConsultingEstablish.MyfatoorahInvoiceId = Trans_res.Data.InvoiceId.ToString();
            consultingEstablishService.Edit(oTbConsultingEstablish);
            return Trans_res;
        }


        [HttpPost("WalletOfferApprovedOfficeRequest")]
        public async Task<ActionResult<InvoiceResponse>> WalletOfferApprovedOfficeRequest([FromForm] MyFatoorahLinkDtos model)
        {
            
            TbCharge oTbCharge = new TbCharge();
            _chargeService.Add(oTbCharge);
            var a = await MyFatoorah.ExecutePayment2(model.PaymentMethodId, model.invoiceValue, oTbCharge.ChargeId.ToString(), model.PaymentGateId, model.UserId, model.UserEmail , model.UserFirstName);
            //var a = await MyFatoorah.ExecutePayment();
            //var executePaymentRequest = new
            //{
            //    //required fields
            //    PaymentMethodId = model.PaymentMethodId,
            //    InvoiceValue = int.Parse(model.invoiceValue),
            //    CallBackUrl = "https://example.com/callback",
            //    ErrorUrl = "https://example.com/error",
            //    //optional fields 
            //    CustomerName = "Customer Name",
            //    DisplayCurrencyIso = "KWD",
            //    MobileCountryCode = "965",
            //    CustomerMobile = "12345678",
            //    CustomerEmail = "email@example.com",
            //    Language = "En",
            //    CustomerReference = "",
            //    CustomerCivilId = "",
            //    UserDefinedField = "",
            //    ExpiryDate = DateTime.Now.AddYears(1),
            //    // to add suppliers
            //    Suppliers = new[] {
            //            new {
            //              SupplierCode = 1, InvoiceShare = 1000, ProposedShare = 500
            //            }
            //     }

            //};
            //var executeRequestJSON = JsonConvert.SerializeObject(executePaymentRequest);
            InvoiceResponse Trans_res = JsonConvert.DeserializeObject<InvoiceResponse>(a);
            //TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(model.ConsultingId)).FirstOrDefault();
           
            oTbCharge.MyfatoorahInvoiceId = Trans_res.Data.InvoiceId.ToString();
            _chargeService.Edit(oTbCharge);
            return Trans_res;
        }
        // PUT api/<MyFatoorahLinkRecievePaymentApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MyFatoorahLinkRecievePaymentApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
