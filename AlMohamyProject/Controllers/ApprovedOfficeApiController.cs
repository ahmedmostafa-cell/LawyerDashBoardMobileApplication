using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovedOfficeApiController : ControllerBase
    {
        EvaluationApprovedOfficeService evaluationApprovedOfficeService;
        ApprovedOfficeService approvedOfficeService;
        AlMohamyDbContext ctx;
        public ApprovedOfficeApiController(EvaluationApprovedOfficeService EvaluationApprovedOfficeService, ApprovedOfficeService ApprovedOfficeService, AlMohamyDbContext context)
        {
            approvedOfficeService = ApprovedOfficeService;
            ctx = context;
            evaluationApprovedOfficeService = EvaluationApprovedOfficeService;

        }
        // GET: api/<ApprovedOfficeApiController>
        [HttpGet]
        //public IEnumerable<ApproveOfficeEvalutionDtos> Get()
        //{
        //    var approveOfficesTransferData = new List<ApproveOfficeEvalutionDtos>();
        //    List<TbApprovedOffice> lstApprovedOffice = approvedOfficeService.getAll().ToList().Where(a => a.ApprovalStatus == "مفعل").ToList();
        //    List<TbEvaluationApprovedOffice> lstEvaluationApprovedOffice = evaluationApprovedOfficeService.getAll();
        //    foreach(var i in lstApprovedOffice) 
        //    {
        //        ApproveOfficeEvalutionDtos element = new ApproveOfficeEvalutionDtos();
        //        element.ApprovedOfficeLogo = i.ApprovedOfficeLogo;
        //        element.ApprovedOfficeId = i.ApprovedOfficeId.ToString();

        //        element.ApprovedOfficeName = i.ApprovedOfficeName;
        //        element.ApprovedOfficeShortDescription = i.ApprovedOfficeShortDescription;
        //        double noOfStars = lstEvaluationApprovedOffice.Where(a => a.ApprovedOfficeId ==  i.ApprovedOfficeId.ToString()).Sum(a => int.Parse(a.StartsNo));
        //        if (noOfStars > 0)
        //        {
        //            double countIfEvaluation = lstEvaluationApprovedOffice.Where(a => a.ApprovedOfficeId == i.ApprovedOfficeId.ToString()).Count();
        //            double evaluation = noOfStars / countIfEvaluation;
        //            element.EvaluationNoOfStatrs = (noOfStars / countIfEvaluation).ToString();
        //            element.EvaluationNumerical = evaluation.ToString();

        //        }
        //        else
        //        {
        //            element.EvaluationNoOfStatrs = "No Evaluation";
        //            element.EvaluationNumerical = "No Evaluation";
        //        }
        //        approveOfficesTransferData.Add(element);
        //    }
           
        //    return approveOfficesTransferData;
        //}

        // GET api/<ApprovedOfficeApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApprovedOfficeApiController>
        [HttpPost]
        public IEnumerable<ApproveOfficeEvalutionDtos> Post([FromForm] string userid)
        {
            var approveOfficesTransferData = new List<ApproveOfficeEvalutionDtos>();
            List<TbApprovedOffice> lstApprovedOffice = approvedOfficeService.getAll().ToList().Where(a => a.ApprovalStatus == "مفعل").ToList();
            List<TbEvaluationApprovedOffice> lstEvaluationApprovedOffice = evaluationApprovedOfficeService.getAll();
            foreach (var i in lstApprovedOffice)
            {
                ApproveOfficeEvalutionDtos element = new ApproveOfficeEvalutionDtos();
                element.ApprovedOfficeLogo = i.ApprovedOfficeLogo;
                element.ApprovedOfficeId = i.ApprovedOfficeId.ToString();
                element.ApprovalStatus = i.ApprovalStatus;
                element.ApprovedOfficeName = i.ApprovedOfficeName;
                element.ApprovedOfficeShortDescription = i.ApprovedOfficeShortDescription;
                double noOfStars = lstEvaluationApprovedOffice.Where(a => a.ApprovedOfficeId == i.ApprovedOfficeId.ToString()).Sum(a => int.Parse(a.StartsNo));
                if (noOfStars > 0)
                {
                    double countIfEvaluation = lstEvaluationApprovedOffice.Where(a => a.ApprovedOfficeId == i.ApprovedOfficeId.ToString()).Count();
                    double evaluation = noOfStars / countIfEvaluation;
                    element.EvaluationNoOfStatrs = (noOfStars / countIfEvaluation).ToString();
                    element.EvaluationNumerical = evaluation.ToString();

                }
                else
                {
                    element.EvaluationNoOfStatrs = "No Evaluation";
                    element.EvaluationNumerical = "No Evaluation";
                }
                approveOfficesTransferData.Add(element);
            }

            return approveOfficesTransferData;
        }

        // PUT api/<ApprovedOfficeApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApprovedOfficeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
