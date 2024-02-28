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
    public class ApproveOfficeCustomerAEvaluationApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        EvaluationApprovedOfficeService _evaluationApprovedOfficeService;
        AlMohamyDbContext ctx;
        public ApproveOfficeCustomerAEvaluationApiController(IAccountRepository accountRepository, EvaluationApprovedOfficeService evaluationApprovedOfficeService, AlMohamyDbContext context)
        {
            _evaluationApprovedOfficeService = evaluationApprovedOfficeService;
            ctx = context;
            _accountRepository = accountRepository;

        }
        // GET: api/<ApproveOfficeCustomerAEvaluationApiController>
        [HttpGet]
        public IEnumerable<TbEvaluationApprovedOffice> Get()
        {
            List<TbEvaluationApprovedOffice> lstEvaluationApprovedOffice = _evaluationApprovedOfficeService.getAll();

            return lstEvaluationApprovedOffice;
        }

        // GET api/<ApproveOfficeCustomerAEvaluationApiController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbEvaluationApprovedOffice> Get(Guid id)
        {
            List<TbEvaluationApprovedOffice> lstEvaluationApprovedOffice = _evaluationApprovedOfficeService.getAll().Where(A => A.ApprovedOfficeId == id.ToString()).ToList();

            return lstEvaluationApprovedOffice;
        }

        [HttpGet("{id}/{option2}")]
        public IEnumerable<TbEvaluationApprovedOffice> Get(Guid id, string option2)
        {
            List<TbEvaluationApprovedOffice> lstEvaluationApprovedOffice = new List<TbEvaluationApprovedOffice>();
            if (id != null && option2 == null)
            {
                lstEvaluationApprovedOffice = _evaluationApprovedOfficeService.getAll().Where(a => a.EvaluaterId == id.ToString()).ToList();
            }
            else if (id != null && option2 != null)
            {
                lstEvaluationApprovedOffice = _evaluationApprovedOfficeService.getAll().Where(A => A.EvaluaterId == id.ToString()).Where(a => a.ApprovedOfficeId == option2.ToString()).ToList();
            }
            else if (id == null && option2 != null)
            {
                lstEvaluationApprovedOffice = _evaluationApprovedOfficeService.getAll().Where(A => A.ApprovedOfficeId == option2.ToString()).ToList();
            }




            return lstEvaluationApprovedOffice;
        }
        // POST api/<ApproveOfficeCustomerAEvaluationApiController>
        [HttpPost("Evaluate")]
        public async Task<IActionResult> EstablishConsultation([FromForm] ApproveOfficeEvalutionDtos model)
        {

            var result = await _accountRepository.EvaluateApproveOffice(model);

            if (result == null)
            {
                return BadRequest("The Evalution Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }

        // PUT api/<ApproveOfficeCustomerAEvaluationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApproveOfficeCustomerAEvaluationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
