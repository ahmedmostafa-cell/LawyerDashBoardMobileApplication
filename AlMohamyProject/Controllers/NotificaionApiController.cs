using BL;
using BL.Interfaces;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificaionApiController : ControllerBase
    {
        private readonly NotificationService notificationService;
        AlMohamyDbContext ctx;
        public NotificaionApiController(NotificationService NotificationService,AlMohamyDbContext context)
        {
            notificationService = NotificationService;
             ctx = context;

        }
        // GET: api/<NotificaionApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NotificaionApiController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TbNotification>> Get(string id)
        {
            return notificationService.getAll().OrderByDescending(a => a.CreatedDate).Where(a=> a.ToWhomId == id).ToList();
        }

        // POST api/<NotificaionApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NotificaionApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificaionApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
