using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EvaluationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        EvaluationService evaluationService;
        AlMohamyDbContext ctx;
        public EvaluationController(UserManager<ApplicationUser> userManager, EvaluationService EvaluationService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            evaluationService = EvaluationService;
            ctx = context;
            _userManager = userManager;

        }
        [Authorize(Roles = "Admin,التقييمات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstEvaluations = evaluationService.getAll();
            ViewBag.customers = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View(model);


        }

        [Authorize(Roles = "Admin,التقييمات")]
        public IActionResult IndexTotal()
        {

            HomePageModel model = new HomePageModel();
            model.lstEvaluations = evaluationService.getAll();
            
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View(model);


        }


        

        [HttpPost]
        public async Task<IActionResult> Save(TbEvaluation ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.EvaluationId == null)
            {


                if (ModelState.IsValid)
                {
                    //foreach (var file in files)
                    //{
                    //    if (file.Length > 0)
                    //    {
                    //        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    //        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    //        using (var stream = System.IO.File.Create(filePaths))
                    //        {
                    //            await file.CopyToAsync(stream);
                    //        }
                    //        ITEM.MainConsultingImage = ImageName;
                    //    }
                    //}





                    var result = evaluationService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Evaluation Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Evaluation Profile  Creating.";
                    }


                }


            }
            else
            {
                if (ModelState.IsValid)
                {
                    //foreach (var file in files)
                    //{
                    //    if (file.Length > 0)
                    //    {
                    //        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    //        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    //        using (var stream = System.IO.File.Create(filePaths))
                    //        {
                    //            await file.CopyToAsync(stream);
                    //        }
                    //        ITEM.MainConsultingImage = ImageName;
                    //    }
                    //}






                    var result = evaluationService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Evaluation Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Evaluation Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstEvaluations = evaluationService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف التقييمات")]
        public IActionResult Delete(Guid id)
        {

            TbEvaluation oldItem = ctx.TbEvaluations.Where(a => a.EvaluationId == id).FirstOrDefault();



            var result = evaluationService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Evaluation Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Evaluation Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstEvaluations = evaluationService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل التقييمات")]
        public IActionResult Form(Guid? id)
        {
            TbEvaluation oldItem = ctx.TbEvaluations.Where(a => a.EvaluationId == id).FirstOrDefault();
            oldItem = ctx.TbEvaluations.Where(a => a.EvaluationId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
