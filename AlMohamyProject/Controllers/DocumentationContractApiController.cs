using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationContractApiController : ControllerBase
    {
        DocumentationOfContractService documentationOfContractService;
        AlMohamyDbContext ctx;
        public DocumentationContractApiController(DocumentationOfContractService DocumentationOfContractService, AlMohamyDbContext context)
        {
            documentationOfContractService = DocumentationOfContractService;
            ctx = context;

        }
        // GET: api/<DocumentationContractApiController>
        [HttpGet]
        public IEnumerable<TbDocumentationOfContract> Get()
        {
            List<TbDocumentationOfContract> lstDocumentationOfContracts = documentationOfContractService.getAll();

            return lstDocumentationOfContracts;
        }
        // GET api/<DocumentationContractApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DocumentationContractApiController>
        [HttpPost]
        public IEnumerable<TbDocumentationOfContract> Post([FromForm] string userid)
        {
            List<TbDocumentationOfContract> lstDocumentationOfContracts = documentationOfContractService.getAll();

            return lstDocumentationOfContracts;
        }

        // PUT api/<DocumentationContractApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DocumentationContractApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
