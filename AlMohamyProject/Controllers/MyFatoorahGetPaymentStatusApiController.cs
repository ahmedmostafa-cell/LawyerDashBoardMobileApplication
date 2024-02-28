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
    public class MyFatoorahGetPaymentStatusApiController : ControllerBase
    {
        private readonly ChargeService chargeService;
        PaymentGateService paymentGateService;
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public MyFatoorahGetPaymentStatusApiController(ChargeService ChargeService,PaymentGateService PaymentGateService, IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
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
            chargeService = ChargeService;
        }
        // GET: api/<MyFatoorahGetPaymentStatusApiController>
        [HttpGet]
        public async Task<ActionResult<string>> Get(string id)
        {
            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(id)).FirstOrDefault();
            string keyType = "invoiceid";
            var a = await MyFatoorahPaymentStatus.GetPaymentStatus(oTbConsultingEstablish.MyfatoorahInvoiceId, keyType);
            PaymentStatus Trans_res = JsonConvert.DeserializeObject<PaymentStatus>(a);
            
            if (Trans_res.Data.InvoiceStatus == "Paid")
            {
                if (oTbConsultingEstablish.ServiceId.ToString() == "e871c68d-f8fe-4d66-8739-9c01c3bc3c29")
                {
                    if (oTbConsultingEstablish.TheDocumentationPaidValue == null)
                    {
                        oTbConsultingEstablish.TheDocumentationPaidValue = Trans_res.Data.InvoiceValue.ToString();
                    }
                    else
                    {
                        decimal oldpay = decimal.Parse(oTbConsultingEstablish.TheDocumentationPaidValue);
                        decimal totlapay = oldpay + Trans_res.Data.InvoiceValue;
                        oTbConsultingEstablish.TheDocumentationPaidValue = totlapay.ToString();
                    }

                    if (decimal.Parse(oTbConsultingEstablish.TheDocumentationPaidValue) >= decimal.Parse(oTbConsultingEstablish.TheTotal))
                    {
                        oTbConsultingEstablish.RequestStatus = "حالية";
                    }
                    if (Trans_res.Data.UserDefinedField.ToString() == "c69cfbe9-5306-4613-af0e-89f8dd46dce7")
                    {
                        oTbConsultingEstablish.PaymentGatesId = Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7");
                        oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().PaymentGateTitle;
                    }
                    else if (Trans_res.Data.UserDefinedField.ToString() == "ee104dce-908c-43e5-b236-2eff4730af01")
                    {
                        oTbConsultingEstablish.PaymentGatesId = Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01");
                        oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().PaymentGateTitle;
                    }
                    else if (Trans_res.Data.UserDefinedField.ToString() == "99dff91e-cdb8-4c13-ad27-f2dcfac9032a")
                    {
                        oTbConsultingEstablish.PaymentGatesId = Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a");
                        oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().PaymentGateTitle;
                    }

                }
                else if (oTbConsultingEstablish.ServiceId.ToString() == "39a0999b-c8b8-4fa0-aec9-434285da8ea6")
                {
                    if (oTbConsultingEstablish.TheConsultingPaidValue == null)
                    {
                        oTbConsultingEstablish.TheConsultingPaidValue = Trans_res.Data.InvoiceValue.ToString();
                    }
                    else
                    {
                        decimal oldpay = decimal.Parse(oTbConsultingEstablish.TheConsultingPaidValue);
                        decimal totlapay = oldpay + Trans_res.Data.InvoiceValue;
                        oTbConsultingEstablish.TheConsultingPaidValue = totlapay.ToString();
                    }
                    decimal aa = decimal.Parse(oTbConsultingEstablish.TheConsultingPaidValue);
                    decimal b = decimal.Parse(oTbConsultingEstablish.TheTotal);
                    if (aa >= b)
                    {
                        oTbConsultingEstablish.RequestStatus = "حالية";
                    }
                    if (Trans_res.Data.UserDefinedField.ToString() == "c69cfbe9-5306-4613-af0e-89f8dd46dce7")
                    {
                        oTbConsultingEstablish.PaymentGatesId = Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7");
                        oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().PaymentGateTitle;
                    }
                    else if (Trans_res.Data.UserDefinedField.ToString() == "ee104dce-908c-43e5-b236-2eff4730af01")
                    {
                        oTbConsultingEstablish.PaymentGatesId = Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01");
                        oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().PaymentGateTitle;
                    }
                    else if (Trans_res.Data.UserDefinedField.ToString() == "99dff91e-cdb8-4c13-ad27-f2dcfac9032a")
                    {
                        oTbConsultingEstablish.PaymentGatesId = Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a");
                        oTbConsultingEstablish.PaymentGatePercent = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitle = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("99dff91e-cdb8-4c13-ad27-f2dcfac9032a")).FirstOrDefault().PaymentGateTitle;
                    }
                }
                else if (oTbConsultingEstablish.ServiceId.ToString() == "d6902f37-784d-4d2a-ae16-cdf8666d0adf")
                {
                    if (oTbConsultingEstablish.CreatedBy == null)
                    {
                        oTbConsultingEstablish.CreatedBy = Trans_res.Data.InvoiceValue.ToString();
                    }
                    else
                    {
                        decimal oldpay = decimal.Parse(oTbConsultingEstablish.CreatedBy);
                        decimal totlapay = oldpay + Trans_res.Data.InvoiceValue;
                        oTbConsultingEstablish.CreatedBy = totlapay.ToString();
                    }

                    if (decimal.Parse(oTbConsultingEstablish.CreatedBy) >= decimal.Parse(oTbConsultingEstablish.TheTotalDelegation))
                    {
                        oTbConsultingEstablish.DelegationStatus = "مدفوع";
                    }
                    if (Trans_res.Data.UserDefinedField.ToString() == "c69cfbe9-5306-4613-af0e-89f8dd46dce7")
                    {
                        oTbConsultingEstablish.PaymentGatesIdDelegation = Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7");
                        oTbConsultingEstablish.PaymentGatePercentDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitleDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("c69cfbe9-5306-4613-af0e-89f8dd46dce7")).FirstOrDefault().PaymentGateTitle;
                    }
                    else if (Trans_res.Data.UserDefinedField.ToString() == "ee104dce-908c-43e5-b236-2eff4730af01")
                    {
                        oTbConsultingEstablish.PaymentGatesIdDelegation = Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01");
                        oTbConsultingEstablish.PaymentGatePercentDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().CreatedBy;
                        oTbConsultingEstablish.PaymentGateTitleDelegation = paymentGateService.getAll().Where(a => a.PaymentGatesId == Guid.Parse("ee104dce-908c-43e5-b236-2eff4730af01")).FirstOrDefault().PaymentGateTitle;
                    }
                    else if (Trans_res.Data.UserDefinedField.ToString() == "99dff91e-cdb8-4c13-ad27-f2dcfac9032a")
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


        [HttpGet("WalletSave")]
        public async Task<ActionResult<string>> Wallet(string id)
        {
            TbCharge oTbCharge = chargeService.getAll().Where(a => a.ChargeId == Guid.Parse(id)).FirstOrDefault();
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
                var result = chargeService.Add(oTbCharge);
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

        // GET api/<MyFatoorahGetPaymentStatusApiController>/5
        //[HttpGet("{id}")]
        //public string Get(string id)
        //{
        //    return id;
        //}

        // POST api/<MyFatoorahGetPaymentStatusApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MyFatoorahGetPaymentStatusApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MyFatoorahGetPaymentStatusApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
