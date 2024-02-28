using AlMohamyProject.Dtos;
using AlMohamyProject.Hubs;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AlMohamyProject.Hubs.NotificationHub;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferApprovedOfficeRequestApiController : ControllerBase
    {
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        ApprovedOfficeService approvedOfficeService;
        private readonly IAccountRepository _accountRepository;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public OfferApprovedOfficeRequestApiController(RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, IHubContext<OrderHub> orderHub, ApprovedOfficeService ApprovedOfficeService ,IAccountRepository accountRepository, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            approvedOfficeService = ApprovedOfficeService;
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            Usermanager = usermanager;
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
            settingService = SettingService;
            _accountRepository = accountRepository;
            realTimeNotifcationService = RealTimeNotifcationService;


            _orderHub = orderHub;
            _notificationHub = notificationHub;
        }
        // GET: api/<OfferApprovedOfficeRequestApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OfferApprovedOfficeRequestApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OfferApprovedOfficeRequestApiController>
        [HttpPost]
        public async Task<IActionResult> OfferApprovedOfficeRequest([FromForm] ApproveOfficeRequestDtos model)
        {


            var result = await _accountRepository.OfferApprovedOfficeRequest(model);

            if (result.Value.MyString == "لم يتم  ارسال طلب اعتماد المكتب")
            {
                return BadRequest("The ApproveOfficeRequest Is Not Saved");

            }
            else
            {
                TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();
                oTbRealTimeNotifcation.NotificationType = "طلب اعتماد مكتب";
                oTbRealTimeNotifcation.NotificationSyntax = model.UserFirsName;


                oTbRealTimeNotifcation.CreatedBy = result.Value.MyModel.ApprovedOfficeId.ToString();
                oTbRealTimeNotifcation.UpdatedBy = "طلب اعتماد مكتب";
                realTimeNotifcationService.Add(oTbRealTimeNotifcation);
                notificationCounter = realTimeNotifcationService.getAll().Count();

                List<MessageObject> messages = new List<MessageObject>();
                foreach (var i in ctx.TbRealTimeNotifcations.ToList())
                {
                    messages.Add(new MessageObject { id = i.RealTimeNotifcationId.ToString(), mesage = i.NotificationType, id2 = i.CreatedBy, type = i.UpdatedBy, time = i.CreatedDate.ToString() });


                }
                _orderHub.Clients.All.SendAsync("newOrder").GetAwaiter().GetResult();
                _notificationHub.Clients.All.SendAsync("LoadNotification", messages, notificationCounter).GetAwaiter().GetResult();

                return Ok(result);

            }




        }

        // PUT api/<OfferApprovedOfficeRequestApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OfferApprovedOfficeRequestApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
