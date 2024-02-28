using BL;
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
    public class LawyerEvaluationBringDataApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        EvaluationService evaluationService;
        AlMohamyDbContext ctx;
        public LawyerEvaluationBringDataApiController(IAccountRepository accountRepository, EvaluationService EvaluationService, AlMohamyDbContext context)
        {
            evaluationService = EvaluationService;
            ctx = context;
            _accountRepository = accountRepository;

        }
        // GET: api/<LawyerEvaluationBringDataApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerEvaluationBringDataApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<TbEvaluation> Get(Guid id)
        //{
        //    List<TbEvaluation> lstEvaluation = evaluationService.getAll().Where(A => A.ToBeEvaluatedId == id.ToString()).ToList();

        //    return lstEvaluation;
        //}

        // POST api/<LawyerEvaluationBringDataApiController>
        [HttpPost]
        public IEnumerable<TbEvaluation> Post([FromForm] Guid id)
        {
            List<TbEvaluation> lstEvaluation = evaluationService.getAll().Where(A => A.ToBeEvaluatedId == id.ToString()).ToList();

            return lstEvaluation;
        }

        // PUT api/<LawyerEvaluationBringDataApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerEvaluationBringDataApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
