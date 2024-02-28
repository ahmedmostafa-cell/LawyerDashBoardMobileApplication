using AlMohamyProject.Dtos;
using AlMohamyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyFatoorahInitiatePaymentApiController : ControllerBase
    {
        // GET: api/<MyFatoorahInitiatePaymentApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MyFatoorahInitiatePaymentApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MyFatoorahInitiatePaymentApiController>
        [HttpPost]
        public async Task<PaymentMethodsResponse> PostAsync([FromForm] string paymentvalue)
        {
            var a = await MyFatoorah.InitiatePayment(paymentvalue);
            PaymentMethodsResponse Trans_res = JsonConvert.DeserializeObject<PaymentMethodsResponse>(a);
            return Trans_res;
        }

        // PUT api/<MyFatoorahInitiatePaymentApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MyFatoorahInitiatePaymentApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
