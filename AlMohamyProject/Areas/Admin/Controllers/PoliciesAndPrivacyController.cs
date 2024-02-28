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
    public class PoliciesAndPrivacyController : Controller
    {
        PoliciesAndPrivacyService policiesAndPrivacyService;
        AlMohamyDbContext ctx;
        public PoliciesAndPrivacyController(PoliciesAndPrivacyService PoliciesAndPrivacyService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            policiesAndPrivacyService = PoliciesAndPrivacyService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,السياسة و الخصوصية")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstPoliciesAndPrivacy = policiesAndPrivacyService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbPoliciesAndPrivacy ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.PoliciesAndPrivacyId == null)
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





                    var result = policiesAndPrivacyService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Policies And Privacy Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Policies And Privacy Profile  Creating.";
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






                    var result = policiesAndPrivacyService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Policies And Privacy Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Policies And Privacy Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstPoliciesAndPrivacy = policiesAndPrivacyService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف السياسة و الخصوصية")]
        public IActionResult Delete(Guid id)
        {

            TbPoliciesAndPrivacy oldItem = ctx.TbPoliciesAndPrivacies.Where(a => a.PoliciesAndPrivacyId == id).FirstOrDefault();



            var result = policiesAndPrivacyService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Policies And Privacy Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Policies And Privacy Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstPoliciesAndPrivacy = policiesAndPrivacyService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل السياسة و الخصوصية")]
        public IActionResult Form(Guid? id)
        {
            TbPoliciesAndPrivacy oldItem = ctx.TbPoliciesAndPrivacies.Where(a => a.PoliciesAndPrivacyId == id).FirstOrDefault();
            oldItem = ctx.TbPoliciesAndPrivacies.Where(a => a.PoliciesAndPrivacyId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
