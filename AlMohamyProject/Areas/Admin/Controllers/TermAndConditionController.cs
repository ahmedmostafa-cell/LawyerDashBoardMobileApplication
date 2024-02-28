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

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TermAndConditionController : Controller
    {
        TermAndConditionService termAndConditionService;
        AlMohamyDbContext ctx;
        public TermAndConditionController(TermAndConditionService TermAndConditionService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            termAndConditionService = TermAndConditionService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,الشروط والاحكام")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstTermAndConditions = termAndConditionService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbTermAndCondition ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.TermsAndConditionsId == null)
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





                    var result = termAndConditionService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Term And Condition Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Term And Condition Profile  Creating.";
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






                    var result = termAndConditionService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Term And Condition Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Term And Condition Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstTermAndConditions = termAndConditionService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الشروط والاحكام")]
        public IActionResult Delete(Guid id)
        {

            TbTermAndCondition oldItem = ctx.TbTermAndConditions.Where(a => a.TermsAndConditionsId == id).FirstOrDefault();



            var result = termAndConditionService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Term And Condition Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Term And Condition Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstTermAndConditions = termAndConditionService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل الشروط والاحكام")]
        public IActionResult Form(Guid? id)
        {
            TbTermAndCondition oldItem = ctx.TbTermAndConditions.Where(a => a.TermsAndConditionsId == id).FirstOrDefault();
            oldItem = ctx.TbTermAndConditions.Where(a => a.TermsAndConditionsId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
