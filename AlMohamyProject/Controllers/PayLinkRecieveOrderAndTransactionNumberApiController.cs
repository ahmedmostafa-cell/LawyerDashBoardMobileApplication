using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;
using AlMohamyProject.Dtos;
using Twilio.Jwt.AccessToken;
using Microsoft.AspNetCore.Identity;
using System.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayLinkRecieveOrderAndTransactionNumberApiController : ControllerBase
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
        public PayLinkRecieveOrderAndTransactionNumberApiController(PaymentGateService PaymentGateService, IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context, ChargeService chargeService)
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
        // GET: api/<PayLinkRecieveOrderAndTransactionNumberApiController>
        [HttpGet]
        public ActionResult<InvoiceDetailsDtos> Get(string orderNumber, string transactionNo)
        {
            //imgsrc userid
            //description is consulting id
            //notes is service id
            //title is payment gate id
            //amount is the total
            //add client name and email and number
            var client1 = new RestClient("https://restpilot.paylink.sa/api/auth");
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("accept", "*/*");
            request1.AddHeader("content-type", "application/json");
            request1.AddParameter("application/json", "{\"persistToken\":true,\"apiId\":\"APP_ID_1632514366953\",\"secretKey\":\"b3fd0e4c-f406-4fd8-8773-ab2ea6007f11\"}", ParameterType.RequestBody);
            IRestResponse response1 = client1.Execute(request1);
            Token_Response tok_res = new JsonDeserializer().Deserialize<Token_Response>(response1);
            if (tok_res.IsSuccess())
            {
                var client = new RestClient("https://restpilot.paylink.sa/api/getInvoice/" + transactionNo + "");
                var request = new RestRequest(Method.GET);
                request.AddHeader("accept", "application/json;charset=UTF-8");
                request.AddHeader("Authorization", tok_res.id_token);
                IRestResponse response = client.Execute(request);
                InvoiceDetailsDtos Trans_res = new JsonDeserializer().Deserialize<InvoiceDetailsDtos>(response);
                TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(Trans_res.GatewayOrderRequest.Products[0].Description)).FirstOrDefault();
                if(Trans_res.OrderStatus == "Paid") 
                {
                    if (Trans_res.GatewayOrderRequest.Note.ToString() == "e871c68d-f8fe-4d66-8739-9c01c3bc3c29")
                    {
                        if (oTbConsultingEstablish.TheDocumentationPaidValue ==null) 
                        {
                            oTbConsultingEstablish.TheDocumentationPaidValue = Trans_res.Amount.ToString();
                        }
                        else 
                        {
                            decimal oldpay = int.Parse(oTbConsultingEstablish.TheDocumentationPaidValue);
                            decimal totlapay = oldpay + Trans_res.Amount;
                            oTbConsultingEstablish.TheDocumentationPaidValue = totlapay.ToString();
                        }
                       
                        if(int.Parse(oTbConsultingEstablish.TheDocumentationPaidValue) >= int.Parse(oTbConsultingEstablish.TheTotal)) 
                        {
                            oTbConsultingEstablish.RequestStatus = "حالية";
                        }
                        if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "c69cfbe9-5306-4613-af0e-89f8dd46dce7")
                        {
                            oTbConsultingEstablish.PaymentGatesId = Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7");
                            oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().PaymentGateTitle;
                        }
                        else if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "ee104dce-908c-43e5-b236-2eff4730af01")
                        {
                            oTbConsultingEstablish.PaymentGatesId = Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01");
                            oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().PaymentGateTitle;
                        }
                        else if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "99dff91e-cdb8-4c13-ad27-f2dcfac9032a")
                        {
                            oTbConsultingEstablish.PaymentGatesId = Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a");
                            oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().PaymentGateTitle;
                        }

                    }
                    else if (Trans_res.GatewayOrderRequest.Note.ToString() == "39a0999b-c8b8-4fa0-aec9-434285da8ea6")
                    {
                        if (oTbConsultingEstablish.TheConsultingPaidValue == null)
                        {
                            oTbConsultingEstablish.TheConsultingPaidValue = Trans_res.Amount.ToString();
                        }
                        else
                        {
                            decimal oldpay = int.Parse(oTbConsultingEstablish.TheConsultingPaidValue);
                            decimal totlapay = oldpay + Trans_res.Amount;
                            oTbConsultingEstablish.TheConsultingPaidValue = totlapay.ToString();
                        }
                        int a = int.Parse(oTbConsultingEstablish.TheConsultingPaidValue);
                        int b = int.Parse(oTbConsultingEstablish.TheTotal);
                        if ( a>=b )
                        {
                            oTbConsultingEstablish.RequestStatus = "حالية";
                        }
                        if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "c69cfbe9-5306-4613-af0e-89f8dd46dce7")
                        {
                            oTbConsultingEstablish.PaymentGatesId = Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7");
                            oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().PaymentGateTitle;
                        }
                        else if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "ee104dce-908c-43e5-b236-2eff4730af01")
                        {
                            oTbConsultingEstablish.PaymentGatesId = Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01");
                            oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().PaymentGateTitle;
                        }
                        else if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "99dff91e-cdb8-4c13-ad27-f2dcfac9032a")
                        {
                            oTbConsultingEstablish.PaymentGatesId = Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a");
                            oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().PaymentGateTitle;
                        }
                    }
                    else if (Trans_res.GatewayOrderRequest.Note.ToString() == "d6902f37-784d-4d2a-ae16-cdf8666d0adf")
                    {
                        if (oTbConsultingEstablish.CreatedBy == null)
                        {
                            oTbConsultingEstablish.CreatedBy = Trans_res.Amount.ToString();
                        }
                        else
                        {
                            decimal oldpay = int.Parse(oTbConsultingEstablish.CreatedBy);
                            decimal totlapay = oldpay + Trans_res.Amount;
                            oTbConsultingEstablish.CreatedBy = totlapay.ToString();
                        }
                       
                        if (int.Parse(oTbConsultingEstablish.CreatedBy) >= int.Parse(oTbConsultingEstablish.TheTotalDelegation))
                        {
                            oTbConsultingEstablish.DelegationStatus = "مدفوع";
                        }
                        if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "c69cfbe9-5306-4613-af0e-89f8dd46dce7")
                        {
                            oTbConsultingEstablish.PaymentGatesIdDelegation = Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7");
                            oTbConsultingEstablish.PaymentGatePercentDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitleDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().PaymentGateTitle;
                        }
                        else if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "ee104dce-908c-43e5-b236-2eff4730af01")
                        {
                            oTbConsultingEstablish.PaymentGatesIdDelegation = Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01");
                            oTbConsultingEstablish.PaymentGatePercentDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitleDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().PaymentGateTitle;
                        }
                        else if (Trans_res.GatewayOrderRequest.Products[0].Title.ToString() == "99dff91e-cdb8-4c13-ad27-f2dcfac9032a")
                        {
                            oTbConsultingEstablish.PaymentGatesIdDelegation = Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a");
                            oTbConsultingEstablish.PaymentGatePercentDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().CreatedBy;
                            oTbConsultingEstablish.PaymentGateTitleDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().PaymentGateTitle;
                        }
                    }

                    consultingEstablishService.Edit(oTbConsultingEstablish);
                    return Ok("The Payment Is Done");

                }
                else 
                {
                    return BadRequest("The Payment is not done");

                }
              
               
            }
            else
            {
                return BadRequest("No Token is generated");
            }
           

           
        }




        [HttpGet("WalletSave")]
        public ActionResult<InvoiceDetailsDtos> Wallet(string orderNumber, string transactionNo)
        {
            //imgsrc nothing//
            //description is userid//
            //notes is service id
            //title is payment gate id//
            //amount is the total//
            //add client name and email and number
            //change url
            var client1 = new RestClient("https://restpilot.paylink.sa/api/auth");
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("accept", "*/*");
            request1.AddHeader("content-type", "application/json");
            request1.AddParameter("application/json", "{\"persistToken\":true,\"apiId\":\"APP_ID_1632514366953\",\"secretKey\":\"b3fd0e4c-f406-4fd8-8773-ab2ea6007f11\"}", ParameterType.RequestBody);
            IRestResponse response1 = client1.Execute(request1);
            Token_Response tok_res = new JsonDeserializer().Deserialize<Token_Response>(response1);
            if (tok_res.IsSuccess())
            {
                var client = new RestClient("https://restpilot.paylink.sa/api/getInvoice/" + transactionNo + "");
                var request = new RestRequest(Method.GET);
                request.AddHeader("accept", "application/json;charset=UTF-8");
                request.AddHeader("Authorization", tok_res.id_token);
                IRestResponse response = client.Execute(request);
                InvoiceDetailsDtos Trans_res = new JsonDeserializer().Deserialize<InvoiceDetailsDtos>(response);
                TbCharge oTbCharge = new TbCharge();
                //TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(Trans_res.GatewayOrderRequest.Products[0].Description)).FirstOrDefault();
                if (Trans_res.OrderStatus == "Paid")
                {
                    oTbCharge.ChargeTypeSender = "عملية شحن";
                    oTbCharge.ChargeValue = Trans_res.Amount.ToString();
                    var a = Trans_res.GatewayOrderRequest.Products.FirstOrDefault();
                    oTbCharge.IdSender = a.Description;
                    oTbCharge.SenderName = Usermanager.Users.Where(a => a.Id == oTbCharge.IdSender).FirstOrDefault().FirstName;
                    oTbCharge.CreatedBy = a.Title;
                 
                    var result =_chargeService.Add(oTbCharge);
                    if(result) 
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
            else
            {
                return BadRequest("No Token is generated");
            }



        }



        // POST api/<PayLinkRecieveOrderAndTransactionNumberApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PayLinkRecieveOrderAndTransactionNumberApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PayLinkRecieveOrderAndTransactionNumberApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
