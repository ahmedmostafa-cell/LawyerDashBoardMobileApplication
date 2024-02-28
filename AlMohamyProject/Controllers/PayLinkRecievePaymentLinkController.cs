using AlMohamyProject.Dtos;
using BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayLinkRecievePaymentLinkController : ControllerBase
    {
        private readonly ChargeService _chargeService;
        PaymentGateService paymentGateService;
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public PayLinkRecievePaymentLinkController(PaymentGateService PaymentGateService, IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context, ChargeService chargeService)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            Usermanager = usermanager;
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
            settingService = SettingService;
            _accountRepository = accountRepository;
            paymentGateService = PaymentGateService;
            _chargeService = chargeService;
        }
        // GET: api/<PayLinkRecievePaymentLinkController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PayLinkRecievePaymentLinkController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PayLinkRecievePaymentLinkController>
        [HttpPost("OfferApprovedOfficeRequest")]
        public ActionResult<string> OfferApprovedOfficeRequest([FromForm] TransactionDtos Model)
        {
            var consult = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(Model.description)).FirstOrDefault();
            //if(consult.OrderStatus != "تم الموافقة من المحامي" && consult.OrderStatus != "تمت الموافقة عل عرض محامي") 
            //{
            //    return BadRequest("There is No Approval Either From Lawyer on The Consult Or From The User On The Offer");
            //}
            var client1 = new RestClient("https://restpilot.paylink.sa/api/auth");
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("accept", "*/*");
            request1.AddHeader("content-type", "application/json");
            request1.AddParameter("application/json", "{\"persistToken\":true,\"apiId\":\"APP_ID_1632514366953\",\"secretKey\":\"b3fd0e4c-f406-4fd8-8773-ab2ea6007f11\"}", ParameterType.RequestBody);
            IRestResponse response1 = client1.Execute(request1);
            Token_Response tok_res = new JsonDeserializer().Deserialize<Token_Response>(response1);
            if (tok_res.IsSuccess()) 
            {
                var client = new RestClient("https://restpilot.paylink.sa/api/addInvoice");
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json;charset=UTF-8");
                request.AddHeader("Authorization", tok_res.id_token);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"products\":[{\"description\":\"" + Model.description + "\",\"imageSrc\":\"" + Model.imageSrc + "\",\"isDigital\":" + Model.isDigital + ",\"price\":" + Model.price + ",\"productCost\":" + Model.productCost + ",\"qty\":" + Model.qty + ",\"specificVat\":" + Model.specificVat + ",\"title\":\"" + Model.title + "\"}],\"amount\":" + Model.amount + ",\"callBackUrl\":\"" + Model.callBackUrl + "\",\"clientEmail\":\"" + Model.clientEmail + "\",\"clientMobile\":\"" + Model.clientMobile + "\",\"clientName\":\"" + Model.clientName + "\",\"note\":\"" + Model.note + "\",\"orderNumber\":\"" + Model.orderNumber + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                PaylinkRecieveDtos res = new JsonDeserializer().Deserialize<PaylinkRecieveDtos>(response);
                return res.url;
            }
            else
            {
                return BadRequest("No Token is generated");
            }



           
           
        }



        // POST api/<PayLinkRecievePaymentLinkController>
        [HttpPost("WalletOfferApprovedOfficeRequest")]
        public ActionResult<string> WalletOfferApprovedOfficeRequest([FromForm] TransactionDtos Model)
        {
            //var consult = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(Model.description)).FirstOrDefault();
            //if (consult.OrderStatus != "تم الموافقة من المحامي" && consult.OrderStatus != "تمت الموافقة عل عرض محامي")
            //{
            //    return BadRequest("There is No Approval Either From Lawyer on The Consult Or From The User On The Offer");
            //}
            var client1 = new RestClient("https://restpilot.paylink.sa/api/auth");
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("accept", "*/*");
            request1.AddHeader("content-type", "application/json");
            request1.AddParameter("application/json", "{\"persistToken\":true,\"apiId\":\"APP_ID_1632514366953\",\"secretKey\":\"b3fd0e4c-f406-4fd8-8773-ab2ea6007f11\"}", ParameterType.RequestBody);
            IRestResponse response1 = client1.Execute(request1);
            Token_Response tok_res = new JsonDeserializer().Deserialize<Token_Response>(response1);
            if (tok_res.IsSuccess())
            {
                var client = new RestClient("https://restpilot.paylink.sa/api/addInvoice");
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json;charset=UTF-8");
                request.AddHeader("Authorization", tok_res.id_token);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"products\":[{\"description\":\"" + Model.description + "\",\"imageSrc\":\"" + Model.imageSrc + "\",\"isDigital\":" + Model.isDigital + ",\"price\":" + Model.price + ",\"productCost\":" + Model.productCost + ",\"qty\":" + Model.qty + ",\"specificVat\":" + Model.specificVat + ",\"title\":\"" + Model.title + "\"}],\"amount\":" + Model.amount + ",\"callBackUrl\":\"" + Model.callBackUrl + "\",\"clientEmail\":\"" + Model.clientEmail + "\",\"clientMobile\":\"" + Model.clientMobile + "\",\"clientName\":\"" + Model.clientName + "\",\"note\":\"" + Model.note + "\",\"orderNumber\":\"" + Model.orderNumber + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                PaylinkRecieveDtos res = new JsonDeserializer().Deserialize<PaylinkRecieveDtos>(response);
                return res.url;
            }
            else
            {
                return BadRequest("No Token is generated");
            }





        }

        // PUT api/<PayLinkRecievePaymentLinkController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PayLinkRecievePaymentLinkController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
