using AlMohamyProject.Dtos;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using Twilio.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayLinkBringTokenApiController : ControllerBase
    {
        // GET: api/<PayLinkBringTokenApiController>
        [HttpGet]
        public string  Get()
        {
            var client = new RestClient("https://restpilot.paylink.sa/api/auth");
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "*/*");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"persistToken\":true,\"apiId\":\"APP_ID_1632514366953\",\"secretKey\":\"b3fd0e4c-f406-4fd8-8773-ab2ea6007f11\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Token_Response tok_res = new JsonDeserializer().Deserialize<Token_Response>(response);
            if(tok_res.IsSuccess()) 
            {
                return tok_res.id_token;
            }
            else 
            {
                return "No Token Is Generated";
            }
            
        }

        // GET api/<PayLinkBringTokenApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PayLinkBringTokenApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PayLinkBringTokenApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PayLinkBringTokenApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
