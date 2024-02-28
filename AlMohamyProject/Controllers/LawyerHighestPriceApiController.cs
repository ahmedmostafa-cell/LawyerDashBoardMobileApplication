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
    public class LawyerHighestPriceApiController : ControllerBase
    {
        EvaluationService evaluationService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public LawyerHighestPriceApiController(EvaluationService EvaluationService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            Usermanager = usermanager;
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
            evaluationService = EvaluationService;
        }
        // GET: api/<LawyerHighestPriceApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerHighestPriceApiController>/5
        [HttpGet("{id}")]
        //public IEnumerable<LawyerChooseDtos> Get(string id)
        //{
        //    var Lwyers = Usermanager.Users.Where(a => a.MainConsultingId == id).ToList();
        //    var lawyersTransferData = new List<LawyerChooseDtos>();
        //    List<TbEvaluation> lstEvaluations = evaluationService.getAll();
        //    foreach (var i in Lwyers)
        //    {
        //        LawyerChooseDtos user = new LawyerChooseDtos();
        //        user.Id = i.Id;
        //        user.Cost = i.Cost;
        //        user.FirstName = i.FirstName;
        //        user.Image = i.Image;
        //        user.ShortDescription = i.ShortDescription;
        //        user.YearsOfExperience = i.YearsOfExperience;
        //        double noOfStars = lstEvaluations.Where(a => a.ToBeEvaluatedId == i.Id).Sum(a => int.Parse(a.StartsNo));
        //        if (noOfStars > 0)
        //        {
        //            double countIfEvaluation = lstEvaluations.Where(a => a.ToBeEvaluatedId == i.Id).Count();
        //            double evaluation = noOfStars / countIfEvaluation;
        //            user.EvaluationNoOfStatrs = (noOfStars / countIfEvaluation).ToString();
        //            user.EvaluationNumerical = evaluation.ToString();

        //        }
        //        else
        //        {
        //            user.EvaluationNoOfStatrs = "No Evaluation";
        //            user.EvaluationNumerical = "No Evaluation";
        //        }
        //        lawyersTransferData.Add(user);
        //    }

        //    return lawyersTransferData.OrderByDescending(a => a.Cost);



        //}

        // POST api/<LawyerHighestPriceApiController>
        [HttpPost]
        public IEnumerable<LawyerChooseDtos> Post([FromForm] string id)
        {
            var Lwyers = Usermanager.Users.Where(a => a.MainConsultingId == id && a.Status == "مفعل").ToList();
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

            return lawyersTransferData.OrderByDescending(a => a.Cost);
        }

        // PUT api/<LawyerHighestPriceApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerHighestPriceApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
