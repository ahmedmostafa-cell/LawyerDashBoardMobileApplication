using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireBaseNotificationApiController : ControllerBase
    {
        public class Notification2
        {
            public string Token { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }
        // GET: api/<FireBaseNotificationApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FireBaseNotificationApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FireBaseNotificationApiController>
        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] Notification2 notification)
        {
            var message = new Message()
            {
                Token = notification.Token,
                Notification = new Notification
                {
                    Title = notification.Title,
                    Body = notification.Body
                }
            };

            var result = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            return Ok(result);
        }

        // PUT api/<FireBaseNotificationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FireBaseNotificationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
