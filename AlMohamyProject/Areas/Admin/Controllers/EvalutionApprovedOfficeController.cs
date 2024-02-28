using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EvalutionApprovedOfficeController : Controller
    {
        ApprovedOfficeService approvedOfficeService;
        private readonly UserManager<ApplicationUser> _userManager;
        EvaluationApprovedOfficeService _evaluationApprovedOfficeService;
        AlMohamyDbContext ctx;
        public EvalutionApprovedOfficeController(ApprovedOfficeService ApprovedOfficeService,UserManager<ApplicationUser> userManager, EvaluationApprovedOfficeService evaluationApprovedOfficeService, MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            _evaluationApprovedOfficeService = evaluationApprovedOfficeService;
            ctx = context;
            _userManager = userManager;
            approvedOfficeService = ApprovedOfficeService;

        }
        [Authorize(Roles = "Admin,تقييمات المكاتب المعتمدة")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstEvaluationApprovedOffices = _evaluationApprovedOfficeService.getAll();
            ViewBag.customers = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            ViewBag.approveoffice = approvedOfficeService.getAll();
            return View(model);


        }


        [Authorize(Roles = "Admin,تقييمات المكاتب المعتمدة")]
        public IActionResult IndexTotal()
        {

            HomePageModel model = new HomePageModel();
            model.lstEvaluationApprovedOffices = _evaluationApprovedOfficeService.getAll();
            ViewBag.customers = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            ViewBag.approveoffice = approvedOfficeService.getAll();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbEvaluationApprovedOffice ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.EvaluationApprovedOfficeId == null)
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





                    var result = _evaluationApprovedOfficeService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Evaluation Approved Office Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Evaluation Approved Office Profile  Creating.";
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






                    var result = _evaluationApprovedOfficeService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Evaluation Approved Office Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Evaluation Approved Office Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstEvaluationApprovedOffices = _evaluationApprovedOfficeService.getAll();
            ViewBag.approveoffice = approvedOfficeService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف تقييمات المكاتب المعتمدة")]
        public IActionResult Delete(Guid id)
        {

            TbEvaluationApprovedOffice oldItem = ctx.TbEvaluationApprovedOffices.Where(a => a.EvaluationApprovedOfficeId == id).FirstOrDefault();



            var result = _evaluationApprovedOfficeService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Evaluation Approved Office Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Evaluation Approved Office Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstEvaluationApprovedOffices = _evaluationApprovedOfficeService.getAll();
            ViewBag.approveoffice = approvedOfficeService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل تقييمات المكاتب المعتمدة")]
        public IActionResult Form(Guid? id)
        {
            TbEvaluationApprovedOffice oldItem = ctx.TbEvaluationApprovedOffices.Where(a => a.EvaluationApprovedOfficeId == id).FirstOrDefault();
            ViewBag.approveoffice = approvedOfficeService.getAll();

            return View(oldItem);
        }
    }
}
