using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerChargeApiController : ControllerBase
    {
        SettingService settingService;
        ChargeService _chargeService;
        AlMohamyDbContext ctx;
        public LawyerChargeApiController(SettingService SettingService,ChargeService chargeService, AlMohamyDbContext context)
        {
            _chargeService = chargeService;
            ctx = context;
            settingService = SettingService;

        }
        // GET: api/<LawyerChargeApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerChargeApiController>/5
        [HttpGet("{id}")]
        //public ActionResult ChargeDtos(string id)
        //{
            
        //    List<TbCharge> lstTbConsultingEstablish = _chargeService.getAll().Where(A => A.IdReciever == id).ToList();
        //    int totalCharge = lstTbConsultingEstablish.Where(a => a.ChargeTypeReciever == "عملية استقبال").Sum(a => int.Parse(a.ChargeValue));

        //    int totlaNetCharge = totalCharge;
        //    int AppProfitPercent = int.Parse(settingService.getAll().First().AppProfitPercent);
        //    int AppProfit = (totalCharge * AppProfitPercent)/100;
        //    ChargeDtos oChargeDtos = new ChargeDtos();
        //    oChargeDtos.NetChargeValue = totlaNetCharge.ToString();
        //    oChargeDtos.AppProfit = AppProfit.ToString();
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
        //    if (oChargeDtos != null)
        //    {
        //        return Ok(oChargeDtos);
        //    }
        //    else
        //    {
        //        return BadRequest("There is No Data");
        //    }


        //}

        // POST api/<LawyerChargeApiController>
        [HttpPost]
        public ActionResult Post([FromForm] string id)
        {
            List<TbCharge> lstTbConsultingEstablish = _chargeService.getAll().Where(A => A.IdReciever == id).ToList();
            int totalCharge = lstTbConsultingEstablish.Where(a => a.ChargeTypeReciever == "عملية استقبال").Sum(a => int.Parse(a.ChargeValue));

            int totlaNetCharge = totalCharge;
            int AppProfitPercent = int.Parse(settingService.getAll().First().AppProfitPercent);
            int AppProfit = (totalCharge * AppProfitPercent) / 100;
            ChargeDtos oChargeDtos = new ChargeDtos();
            oChargeDtos.NetChargeValue = totlaNetCharge.ToString();
            oChargeDtos.AppProfit = AppProfit.ToString();
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
                oTransactionElement.RequestNo = i.UpdatedBy;
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

        // PUT api/<LawyerChargeApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerChargeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
