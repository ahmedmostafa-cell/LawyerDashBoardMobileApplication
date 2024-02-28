using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerAppointApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        LawyerAppintmentsService lawyerAppintmentsService;
        AlMohamyDbContext ctx;
        public LawyerAppointApiController(UserManager<ApplicationUser> usermanager, LawyerAppintmentsService LawyerAppintmentsService, AlMohamyDbContext context)
        {
            lawyerAppintmentsService = LawyerAppintmentsService;
            ctx = context;
            Usermanager = usermanager;

        }
        // GET: api/<LawyerAppointApiController>
        [HttpGet]
        public IEnumerable<TbLawyerAppintments> Get()
        {
            return lawyerAppintmentsService.getAll();
        }

        // GET api/<LawyerAppointApiController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbLawyerAppintments> Get(string id)
        {
            return lawyerAppintmentsService.getAll().Where(a=> a.LawyerId == id);
        }
        [HttpPost]
        public IEnumerable<TbLawyerAppintments> Post([FromForm] string id)
        {
            return lawyerAppintmentsService.getAll().Where(a => a.LawyerId == id);
        }

        // POST api/<LawyerAppointApiController>
        [HttpPost("SendAppoints")]
        public async Task<IActionResult> PostAsync([FromBody] LawyerAppointDtos model)
        {
            foreach(var i in lawyerAppintmentsService.getAll().Where(a=> a.LawyerId == model.LawyerId)) 
            {
                lawyerAppintmentsService.Delete(i);
            }
            var user =  Usermanager.Users.Where(a=> a.Id == model.LawyerId).FirstOrDefault();


            int count = 0;
            var result = false;
            foreach (var i in model.DayDetails)
            {
                TbLawyerAppintments oTbLawyerAppintments = new TbLawyerAppintments();

                oTbLawyerAppintments.LawyerId = model.LawyerId;
                oTbLawyerAppintments.LawyerName = user.FirstName;
                oTbLawyerAppintments.WeekDay = i.WeekDay;
                oTbLawyerAppintments.FromHour = double.Parse(i.FromHour);
                oTbLawyerAppintments.MorEveFrst = i.MorEveFrst;
                oTbLawyerAppintments.ToHour = double.Parse(i.ToHour);
                oTbLawyerAppintments.MorEveScond = i.MorEveScond;
                oTbLawyerAppintments.Notes = i.Date;
                result = lawyerAppintmentsService.Add(oTbLawyerAppintments);
                count++;



            }
            if (count != model.DayDetails.Count)
            {
                return Unauthorized();

            }
            return Ok(new { Result = lawyerAppintmentsService.getAll().Where(a=> a.LawyerId == model.LawyerId), AdditionalInfo = "The Data Is Saved" });

        }

        // PUT api/<LawyerAppointApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerAppointApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
