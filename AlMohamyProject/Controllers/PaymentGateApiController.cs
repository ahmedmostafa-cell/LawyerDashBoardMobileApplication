using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGateApiController : ControllerBase
    {
        PaymentGateService _paymentGateService;
        AlMohamyDbContext ctx;
        public PaymentGateApiController(PaymentGateService paymentGateService, AlMohamyDbContext context)
        {
            _paymentGateService = paymentGateService;
            ctx = context;
         
        }
        // GET: api/<PaymentGateApiController>
        [HttpGet]
        public IEnumerable<TbPaymentGates> Get()
        {
            List<TbPaymentGates> lstPaymentGates = _paymentGateService.getAll().ToList().Where(a => a.ActivationStatus == "مفعل").ToList();

            return lstPaymentGates;
        }

        // GET api/<PaymentGateApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PaymentGateApiController>
        [HttpPost]
        public IEnumerable<TbPaymentGates> Post([FromForm] string id)
        {
            List<TbPaymentGates> lstPaymentGates = _paymentGateService.getAll().ToList().Where(a => a.ActivationStatus == "مفعل").ToList();

            return lstPaymentGates;
        }

        // PUT api/<PaymentGateApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PaymentGateApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
