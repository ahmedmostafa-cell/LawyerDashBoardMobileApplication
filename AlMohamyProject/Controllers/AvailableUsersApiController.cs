using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableUsersApiController : ControllerBase
    {
        // GET: api/<AvailableUsersApiController>
        [HttpGet]
        public int Get()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 301); // generates a random integer between 1 (inclusive) and 301 (exclusive)
            Console.WriteLine(randomNumber);

            return randomNumber;

        }

        // GET api/<AvailableUsersApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AvailableUsersApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AvailableUsersApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AvailableUsersApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
