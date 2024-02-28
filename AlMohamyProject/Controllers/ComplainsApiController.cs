using AlMohamyProject.Hubs;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static AlMohamyProject.Hubs.NotificationHub;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using AlMohamyProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplainsApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        ComplainsAndSuggestionsService complainsAndSuggestionsService;
        AlMohamyDbContext ctx;
        public ComplainsApiController(UserManager<ApplicationUser> usermanager, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, IHubContext<OrderHub> orderHub, ComplainsAndSuggestionsService ComplainsAndSuggestionsService, AlMohamyDbContext context)
        {
            complainsAndSuggestionsService = ComplainsAndSuggestionsService;
            ctx = context;

            Usermanager = usermanager;
            realTimeNotifcationService = RealTimeNotifcationService;
          
           
            _orderHub = orderHub;
            _notificationHub = notificationHub;

        }
        // GET: api/<ComplainsApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ComplainsApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ComplainsApiController>
        [HttpPost("SendComplain")]
        public IActionResult Post([FromForm] SendComplainModel sendComplainModel)
        {
            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();
            TbComplainsAndSuggestion oTbComplain = new TbComplainsAndSuggestion();

            oTbComplain.Name = sendComplainModel.Name;
            oTbComplain.CreatedDate = sendComplainModel.CreatedDate;
            oTbComplain.Idd = sendComplainModel.Id;
            oTbComplain.Email = sendComplainModel.Email;
            
            oTbComplain.ComplaintsAndSuggestionsText = sendComplainModel.ComplaintsAndSuggestionsText;
            oTbRealTimeNotifcation.NotificationType = "شكوي من عميل";
            oTbRealTimeNotifcation.NotificationSyntax = oTbComplain.ComplaintsAndSuggestionsText;

            Guid? result = complainsAndSuggestionsService.Add(oTbComplain);
            oTbRealTimeNotifcation.CreatedBy = result.ToString();
            oTbRealTimeNotifcation.UpdatedBy = "شكوي من عميل";

            if (result == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return Unauthorized();

            }
            realTimeNotifcationService.Add(oTbRealTimeNotifcation);
            notificationCounter = realTimeNotifcationService.getAll().Count();

            List<MessageObject> messages = new List<MessageObject>();
            foreach (var i in ctx.TbRealTimeNotifcations.ToList())
            {
                messages.Add(new MessageObject { id = i.RealTimeNotifcationId.ToString(), mesage = i.NotificationType, id2 = i.CreatedBy, type = i.UpdatedBy, time = i.CreatedDate.ToString() });


            }
            _orderHub.Clients.All.SendAsync("newOrder").GetAwaiter().GetResult();
            _notificationHub.Clients.All.SendAsync("LoadNotification", messages, notificationCounter).GetAwaiter().GetResult();
            //_notificationHub.Clients.All.SendAsync("")


            return Ok(result);
        }

        // PUT api/<ComplainsApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ComplainsApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
