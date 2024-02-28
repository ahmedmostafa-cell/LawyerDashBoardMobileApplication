using AlMohamyProject.Dtos;
using BL;
using Domains;
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
    public class CustomerEvaluationApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        EvaluationService evaluationService;
        AlMohamyDbContext ctx;
        public CustomerEvaluationApiController(IAccountRepository accountRepository, EvaluationService EvaluationService, AlMohamyDbContext context)
        {
            evaluationService = EvaluationService;
            ctx = context;
            _accountRepository = accountRepository;

        }
        // GET: api/<CustomerEvaluationApiController>
       
        [HttpGet]
        public IEnumerable<TbEvaluation> Get()
        {
            List<TbEvaluation> lstEvaluation = evaluationService.getAll();

            return lstEvaluation;
        }

        // GET api/<CustomerEvaluationApiController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbEvaluation> Get(Guid id)
        {
            List<TbEvaluation> lstEvaluation = evaluationService.getAll().Where(A => A.EvaluaterId ==  id.ToString()).ToList();

            return lstEvaluation;
        }
        [HttpGet("Lawyer/{id}")]
        public IEnumerable<TbEvaluation> GetLawyer(Guid id)
        {
            List<TbEvaluation> lstEvaluation = evaluationService.getAll().Where(A => A.ToBeEvaluatedId == id.ToString()).ToList();

            return lstEvaluation;
        }


        [HttpGet("{id}/{option2}")]
        public IEnumerable<TbEvaluation> Get(Guid id, string option2)
        {
            List<TbEvaluation> lstEvaluation = new List<TbEvaluation>();
            if (id != null && option2 == null)
            {
                lstEvaluation = evaluationService.getAll().Where(a => a.EvaluaterId == id.ToString()).ToList();
            }
            else if (id != null && option2 != null)
            {
                lstEvaluation = evaluationService.getAll().Where(A => A.EvaluaterId == id.ToString()).Where(a => a.ToBeEvaluatedId == option2.ToString()).ToList();
            }
            else if (id == null && option2 != null)
            {
                lstEvaluation = evaluationService.getAll().Where(A => A.ToBeEvaluatedId == option2.ToString()).ToList();
            }
          



            return lstEvaluation;
        }

        // POST api/<CustomerEvaluationApiController>
        [HttpPost("Evaluate")]
        public async Task<IActionResult> EstablishConsultation([FromForm] EaluationDTos model)
        {

            var result = await _accountRepository.Evaluate(model);

            if (result == null)
            {
                return BadRequest("The Files Are Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }

        // PUT api/<CustomerEvaluationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerEvaluationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
