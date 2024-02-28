using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ConsultingEstablishService consultingEstablishService;
        ChargeService _chargeService;
        AlMohamyDbContext ctx;
        public ChargeApiController(ChargeService chargeService, AlMohamyDbContext context, ConsultingEstablishService ConsultingEstablishService, UserManager<ApplicationUser> UserManager)
        {
            _chargeService = chargeService;
            ctx = context;
            consultingEstablishService = ConsultingEstablishService;
            userManager = UserManager;
        }
        // GET: api/<ChargeApiController>
        [HttpGet]
        public string Get()
        {
           

            var TotalSalesPerEmployee = ctx.VwChargeDeducts.Where(a=> a.ChargeTypeSender == "عملية شحن").ToList();
            List<ChargeGateDeductDtos> lst = new List<ChargeGateDeductDtos>();
        
            foreach (var i in TotalSalesPerEmployee) 
            {
                ChargeGateDeductDtos oChargeGateDeductDtos = new ChargeGateDeductDtos();
                oChargeGateDeductDtos.PaymentGatesId =Guid.Parse(i.CreatedBy);
                oChargeGateDeductDtos.Percent = ctx.TbPaymentGatess.Where(a => a.PaymentGatesId == oChargeGateDeductDtos.PaymentGatesId).FirstOrDefault().CreatedBy;
                oChargeGateDeductDtos.TotalCharges = i.totalCharges;
                lst.Add(oChargeGateDeductDtos);
            }
            decimal totalDeductionOnCharge = 0;
            foreach(var i in lst) 
            {
                totalDeductionOnCharge += ((i.TotalCharges * decimal.Parse(i.Percent))/100);
            }
            decimal totalPercentDeduct = totalDeductionOnCharge / lst.Sum(a => a.TotalCharges)*100;
            decimal valuePaid = ctx.VwChargeDeducts.Where(a=> a.ChargeTypeSender == "عملية دفع").Sum(a=> a.totalCharges);
            decimal valueDeducted = (valuePaid * totalPercentDeduct) / 100;
            string deduct = valueDeducted.ToString();
            return deduct;
        }
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
            decimal valuePaid = ctx.VwChargeDeducts.Where(a => a.ChargeTypeSender == "عملية دفع").Where(a=> a.UpdatedBy == id.ToString()).Sum(a => a.totalCharges);
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
           

            List<VwChargeDeduct> lstLogHistories = ctx.VwChargeDeducts.Where(a => a.ChargeTypeSender == "عملية دفع").Where(a => a.UpdatedBy == id.ToString()).ToList();
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

        // GET api/<ChargeApiController>/5

        //public ActionResult ChargeDtos  (string id)
        //{
        //    List<TbCharge> lstTbConsultingEstablish = _chargeService.getAll().Where(A => A.IdSender == id).ToList();
        //    int totalCharge = lstTbConsultingEstablish.Where(a=> a.ChargeTypeSender == "عملية شحن").Sum(a => int.Parse(a.ChargeValue));
        //    int totalPay = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية دفع").Sum(a => int.Parse(a.ChargeValue));
        //    int totalRefund = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية استرجاع").Sum(a => int.Parse(a.ChargeValue));
        //    int totlaNetCharge = (totalCharge + totalRefund) - totalPay;
        //    ChargeDtos oChargeDtos = new ChargeDtos();
        //    oChargeDtos.NetChargeValue = totlaNetCharge.ToString();
        //    oChargeDtos.lstTransactionElements = new List<TransactionElement>();
        //    foreach (var i in lstTbConsultingEstablish) 
        //    {
        //        TransactionElement oTransactionElement = new TransactionElement();
        //        oTransactionElement.Notes = i.Notes;
        //        oTransactionElement.ChargeTypeSender = i.ChargeTypeSender;
        //        oTransactionElement.ChargeValue = i.ChargeValue;
        //        oTransactionElement.ChargeId = i.ChargeId;
        //        oTransactionElement.IdSender = i.IdSender;
        //        oTransactionElement.SenderName = i.SenderName;
        //        oTransactionElement.ChargeTypeReciever = i.ChargeTypeReciever;
        //        oTransactionElement.ConsultingId = i.ConsultingId;
        //        oTransactionElement.IdReciever = i.IdReciever;
        //        oTransactionElement.RecieverName = i.RecieverName;
        //        oTransactionElement.UpdatedDate = i.UpdatedDate;
        //        oTransactionElement.CreatedDate = i.CreatedDate;
        //        oTransactionElement.ConsultingDateAndTime = i.CreatedBy;
        //        oTransactionElement.CurrentState = i.CurrentState;
        //        oTransactionElement.RequestNo = i.UpdatedBy;
        //        oChargeDtos.lstTransactionElements.Add(oTransactionElement);

        //    }
        //    if(oChargeDtos !=null) 
        //    {
        //        return Ok(oChargeDtos);
        //    }
        //    else 
        //    {
        //        return BadRequest("There is No Data");
        //    }


        //}

        // POST api/<ChargeApiController>
        [HttpPost]
        public ActionResult ChargeDtos ([FromForm] string id)
        {
            List<TbCharge> lstTbConsultingEstablish = _chargeService.getAll().Where(A => A.IdSender == id).ToList();
            decimal totalCharge = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية شحن").Sum(a => decimal.Parse(a.ChargeValue));
            decimal totalPay = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية دفع").Sum(a => decimal.Parse(a.ChargeValue));
            decimal totalRefund = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية استرجاع").Sum(a => decimal.Parse(a.ChargeValue));
            decimal totlaNetCharge = (totalCharge + totalRefund) - totalPay;
            ChargeDtos oChargeDtos = new ChargeDtos();
            oChargeDtos.NetChargeValue = totlaNetCharge.ToString();
            oChargeDtos.lstTransactionElements = new List<TransactionElement>();
            foreach (var i in lstTbConsultingEstablish)
            {
                TransactionElement oTransactionElement = new TransactionElement();
                oTransactionElement.Notes = i.Notes;
                oTransactionElement.ChargeTypeSender = i.ChargeTypeSender;
                oTransactionElement.ChargeValue = i.ChargeValue;
                oTransactionElement.ChargeId = i.ChargeId;
                oTransactionElement.IdSender = i.IdSender;
                oTransactionElement.SenderName = i.SenderName;
                oTransactionElement.ChargeTypeReciever = i.ChargeTypeReciever;
                oTransactionElement.ConsultingId = i.ConsultingId;
                oTransactionElement.IdReciever = i.IdReciever;
                oTransactionElement.RecieverName = i.RecieverName;
                oTransactionElement.UpdatedDate = i.UpdatedDate;
                oTransactionElement.CreatedDate = i.CreatedDate;
                oTransactionElement.ConsultingDateAndTime = i.CreatedBy;
                oTransactionElement.CurrentState = i.CurrentState;
                if(i.ConsultingId != null) 
                {
                    oTransactionElement.RequestNo = consultingEstablishService.getAll().Where(a => a.ConsultingId == i.ConsultingId).FirstOrDefault().RequestNo;
                }
               
                oChargeDtos.lstTransactionElements.Add(oTransactionElement);

            }
            if (oChargeDtos != null)
            {
                return Ok(oChargeDtos);
            }
            else
            {
                return BadRequest("There is No Data");
            }
        }

        [HttpPost("WalletPay")]
        public ActionResult WalletPay([FromForm] WallletPayDtos Item)
        {
            List<TbCharge> lstTbConsultingEstablish = _chargeService.getAll().Where(A => A.IdSender == Item.id).ToList();
            decimal totalCharge = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية شحن").Sum(a => decimal.Parse(a.ChargeValue));
            decimal totalPay = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية دفع").Sum(a => decimal.Parse(a.ChargeValue));
            decimal totalRefund = lstTbConsultingEstablish.Where(a => a.ChargeTypeSender == "عملية استرجاع").Sum(a => decimal.Parse(a.ChargeValue));
            decimal totlaNetCharge = (totalCharge + totalRefund) - totalPay;
            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.ConsultingId == Guid.Parse(Item.Consultingid)).FirstOrDefault();
            TbCharge oTbCharge = new TbCharge();
            oTbCharge.SenderName = userManager.Users.Where(a => a.Id == oTbConsultingEstablish.UserId).FirstOrDefault().FirstName;
            oTbCharge.ChargeTypeReciever = "عملية استقبال";
            oTbCharge.ChargeTypeSender = "عملية دفع";
            oTbCharge.ConsultingId = oTbConsultingEstablish.ConsultingId;
            oTbCharge.IdReciever = userManager.Users.Where(a => a.Id == oTbConsultingEstablish.LawyerId).FirstOrDefault().Id;
            oTbCharge.IdSender = userManager.Users.Where(a => a.Id == oTbConsultingEstablish.UserId).FirstOrDefault().Id;
            oTbCharge.RecieverName = userManager.Users.Where(a => a.Id == oTbConsultingEstablish.LawyerId).FirstOrDefault().FirstName;
            
                if (oTbConsultingEstablish.ServiceId == Guid.Parse("e871c68d-f8fe-4d66-8739-9c01c3bc3c29"))
                {
                    if (totlaNetCharge >= decimal.Parse(oTbConsultingEstablish.TheTotal))
                    {
                        if (oTbConsultingEstablish.TheDocumentationPaidValue == null)
                        {
                            oTbConsultingEstablish.TheDocumentationPaidValue = Item.WalletPay;
                            oTbConsultingEstablish.PaymentGateTitle = "المحفظة";
                            oTbCharge.ChargeValue = Item.WalletPay;
                            oTbCharge.UpdatedBy = oTbConsultingEstablish.ServiceId.ToString();
                        }
                        else
                        {
                            decimal oldpay = int.Parse(oTbConsultingEstablish.TheDocumentationPaidValue);
                            decimal totlapay = oldpay + decimal.Parse(Item.WalletPay);
                            oTbConsultingEstablish.TheDocumentationPaidValue = totlapay.ToString();
                        }

                        if (decimal.Parse(oTbConsultingEstablish.TheDocumentationPaidValue) >= decimal.Parse(oTbConsultingEstablish.TheTotal))
                        {
                            oTbConsultingEstablish.RequestStatus = "حالية";
                        }


                    }
                    else
                    {
                        return Ok("The Payment Is Not Done");
                    }


                }
                else if (oTbConsultingEstablish.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6"))
                {
                    if (totlaNetCharge >= decimal.Parse(oTbConsultingEstablish.TheTotal)) 
                    {
                        if (oTbConsultingEstablish.TheConsultingPaidValue == null)
                        {
                            oTbConsultingEstablish.TheConsultingPaidValue = Item.WalletPay;
                            oTbCharge.ChargeValue = Item.WalletPay;
                            oTbCharge.UpdatedBy = oTbConsultingEstablish.ServiceId.ToString();
                            oTbConsultingEstablish.PaymentGateTitle = "المحفظة";
                        }
                        else
                        {
                            decimal oldpay = decimal.Parse(oTbConsultingEstablish.TheConsultingPaidValue);
                            decimal totlapay = oldpay + decimal.Parse(Item.WalletPay);
                            oTbConsultingEstablish.TheConsultingPaidValue = totlapay.ToString();
                        }

                        if (decimal.Parse(oTbConsultingEstablish.TheConsultingPaidValue) >= decimal.Parse(oTbConsultingEstablish.TheTotal))
                        {
                            oTbConsultingEstablish.RequestStatus = "حالية";
                        }

                    }
                    else
                    {
                        return Ok("The Payment Is Not Done");
                    }


                }
                else if (oTbConsultingEstablish.ServiceId == Guid.Parse("d6902f37-784d-4d2a-ae16-cdf8666d0adf"))
                {
                    if (totlaNetCharge >= decimal.Parse(oTbConsultingEstablish.TheTotalDelegation)) 
                    {
                        if (oTbConsultingEstablish.CreatedBy == null)
                        {
                            oTbCharge.UpdatedBy = oTbConsultingEstablish.ServiceId.ToString();
                            oTbConsultingEstablish.CreatedBy = Item.WalletPay;
                            oTbCharge.ChargeValue = Item.WalletPay;
                            oTbConsultingEstablish.PaymentGateTitleDelegation = "المحفظة";
                        }
                        else
                        {
                            decimal oldpay = decimal.Parse(oTbConsultingEstablish.CreatedBy);
                            decimal totlapay = oldpay + decimal.Parse(Item.WalletPay);
                            oTbConsultingEstablish.CreatedBy = totlapay.ToString();
                        oTbConsultingEstablish.PaymentGateTitleDelegation = "المحفظة";

                        }

                        if (decimal.Parse(oTbConsultingEstablish.CreatedBy) >= decimal.Parse(oTbConsultingEstablish.TheTotalDelegation))
                        {
                            oTbConsultingEstablish.DelegationStatus = "مدفوع";
                        }


                    }
                    else
                    {
                        return Ok("The Payment Is Not Done");
                    }

                }
                consultingEstablishService.Edit(oTbConsultingEstablish);
                _chargeService.Add(oTbCharge);
                return Ok("The Payment Is Done");
            
           
                

          
           
        }

        // PUT api/<ChargeApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChargeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
