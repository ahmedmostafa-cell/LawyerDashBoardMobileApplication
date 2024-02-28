using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerChooseApiController : ControllerBase
    {
        private readonly LawyersMainConsultingsService _mainConsultingsService;
        EvaluationService evaluationService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        UserManager<ApplicationUser> Usermanager;
        ConsultingEstablishService consultingEstablishService;
        AlMohamyDbContext ctx;
        public LawyerChooseApiController(LawyersMainConsultingsService mainConsultingsService,EvaluationService EvaluationService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, UserManager<ApplicationUser> usermanager, ConsultingEstablishService ConsultingEstablishService, AlMohamyDbContext context)
        {
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            Usermanager = usermanager;
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
            evaluationService = EvaluationService;
            _mainConsultingsService = mainConsultingsService;
        }
        // GET: api/<LawyerChooseApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LawyerChooseApiController>/5
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

        //    return lawyersTransferData;
           
                

        //}

        // POST api/<LawyerChooseApiController>
        [HttpPost]
        public IEnumerable<LawyerChooseDtos> Post([FromForm] string id)
        {
            //List<TbLawyersMainConsultings> lstLawyeerConsult = _mainConsultingsService.getAll().Where(a => a.MainConsultingId == Guid.Parse(id)).ToList();
            var lstLawyeerConsultgriuped = (from t in _mainConsultingsService.getAll().Where(a => a.MainConsultingId == Guid.Parse(id)).ToList()
                                            group t by t.LawyerId into myVar
                                         select new
                                         {
                                             LawyerId = myVar.Key,
                                             count = myVar.Count()

                                         });
            List<string> lstIdsMainCinsulting = new List<string>();
            foreach (var con in lstLawyeerConsultgriuped)
            {
                lstIdsMainCinsulting.Add(con.LawyerId);
            }
            var Lwyers = Usermanager.Users.Where(a => a.Status == "مفعل" && a.IsActivated ==true).ToList();
            var Lwyers2 = new List<ApplicationUser>();
            foreach (var i in lstIdsMainCinsulting)
            {
                foreach (var el in Lwyers)
                {
                    if (el.Id == i)
                    {
                        Lwyers2.Add(el);
                    }
                }

            }
            var lawyersTransferData = new List<LawyerChooseDtos>();
            List<TbEvaluation> lstEvaluations = evaluationService.getAll();
            foreach (var i in Lwyers2)
            {
                LawyerChooseDtos user = new LawyerChooseDtos();
                user.Id = i.Id;
                user.Cost = i.Cost;
                user.FirstName = i.FirstName;
                user.FamilyName = i.FamilyName;
                user.Image = i.Image;
                user.ShortDescription = i.ShortDescription;
                user.YearsOfExperience = i.YearsOfExperience;
                double noOfStars = lstEvaluations.Where(a => a.ToBeEvaluatedId == i.Id).Sum(a => double.Parse(a.StartsNo));
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

            return lawyersTransferData;
        }


        [HttpPost("bringLawyerData")]
        public LawyerChooseDtos PostBringLawyerData([FromForm] string id)
        {
           
            var Lwyer = Usermanager.Users.Where(a => a.Id == id && a.Status == "مفعل" && a.IsActivated == true).FirstOrDefault();
            var lawyerTransferData = new LawyerChooseDtos();
            lawyerTransferData.consultationNo = consultingEstablishService.getAll().Where(a => a.LawyerId == id && a.RequestStatus == "منتهية").ToList().Count().ToString();
            List<TbEvaluation> lstEvaluations = evaluationService.getAll();
           
                LawyerChooseDtos user = new LawyerChooseDtos();
                user.Id = Lwyer.Id;
                user.Cost = Lwyer.Cost;
                user.FirstName = Lwyer.FirstName;
                user.Image = Lwyer.Image;
                user.ShortDescription = Lwyer.ShortDescription;
                user.YearsOfExperience = Lwyer.YearsOfExperience;
                double noOfStars = lstEvaluations.Where(a => a.ToBeEvaluatedId == Lwyer.Id).Sum(a => int.Parse(a.StartsNo));
                if (noOfStars > 0)
                {
                    double countIfEvaluation = lstEvaluations.Where(a => a.ToBeEvaluatedId == Lwyer.Id).Count();
                    double evaluation = noOfStars / countIfEvaluation;
                    user.EvaluationNoOfStatrs = (noOfStars / countIfEvaluation).ToString();
                    user.EvaluationNumerical = evaluation.ToString();

                }
                else
                {
                    user.EvaluationNoOfStatrs = "No Evaluation";
                    user.EvaluationNumerical = "No Evaluation";
                }
              
           

            return user;
        }
        // PUT api/<LawyerChooseApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LawyerChooseApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
