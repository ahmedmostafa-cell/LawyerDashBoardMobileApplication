using AlMohamyProject.Dtos;
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
    public class SearchForLawyersApiController : ControllerBase
    {
        UserManager<ApplicationUser> Usermanager;
        EvaluationService evaluationService;
        AlMohamyDbContext ctx;
        public SearchForLawyersApiController(EvaluationService EvaluationService, UserManager<ApplicationUser> usermanager,  AlMohamyDbContext context)
        {
            evaluationService = EvaluationService;
            ctx = context;
            Usermanager = usermanager;

        }
        // GET: api/<SearchForLawyersApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SearchForLawyersApiController>/5
       
        [HttpGet("{id}")]
        //public ActionResult<IEnumerable<LawyerSearchDtos>> Get(string id)
        //{
        //    var Lwyers = Usermanager.Users.Where(a=> a.FirstName.Contains(id)).ToList();
        //    var LawyerSearchDtos = new List<LawyerSearchDtos>();
        //    foreach (var i in Lwyers) 
        //    {
        //        LawyerSearchDtos element = new LawyerSearchDtos();
        //        element.Id = i.Id;
        //        element.FirstName = i.FirstName;
        //        element.FamilyName = i.FamilyName;
        //        LawyerSearchDtos.Add(element);
        //    }
        //    if(LawyerSearchDtos != null) 
        //    {
        //        return LawyerSearchDtos;
        //    }
        //    else 
        //    {
        //        return BadRequest("No Lawyers");
        //    }
                
        //}

        // POST api/<SearchForLawyersApiController>
        [HttpPost]
        public ActionResult<IEnumerable<LawyerChooseDtos>> Post([FromForm] string id)
        {
            var Lwyers = Usermanager.Users.Where(a => a.FirstName.Contains(id)).ToList();
            var lawyersTransferData = new List<LawyerChooseDtos>();
            List<TbEvaluation> lstEvaluations = evaluationService.getAll();
            foreach (var i in Lwyers)
            {
                LawyerChooseDtos user = new LawyerChooseDtos();
                user.Id = i.Id;
                user.Cost = i.Cost;
                user.FirstName = i.FirstName;
                user.Image = i.Image;
                user.ShortDescription = i.ShortDescription;
                user.YearsOfExperience = i.YearsOfExperience;
                double noOfStars = lstEvaluations.Where(a => a.ToBeEvaluatedId == i.Id).Sum(a => int.Parse(a.StartsNo));
                if (noOfStars > 0)
                {
                    double countIfEvaluation = lstEvaluations.Where(a => a.ToBeEvaluatedId == i.Id).Count();
                    double evaluation = noOfStars / countIfEvaluation;
                    user.EvaluationNoOfStatrs = (noOfStars / countIfEvaluation).ToString();
                    user.EvaluationNumerical = evaluation.ToString();

                }
                else
                {
                    user.EvaluationNoOfStatrs = "No Evaluation";
                    user.EvaluationNumerical = "No Evaluation";
                }
                lawyersTransferData.Add(user);
            }
            if (lawyersTransferData != null)
            {
                return lawyersTransferData;
            }
            else
            {
                return BadRequest(lawyersTransferData);
            }
        }

        // PUT api/<SearchForLawyersApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchForLawyersApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
