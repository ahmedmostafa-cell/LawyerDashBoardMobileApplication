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
    public class AboutAppController : Controller
    {
        AboutAppService aboutAppService;
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public AboutAppController(AboutAppService AboutAppService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            mainConsultingService = MainConsultingService;
            aboutAppService = AboutAppService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,عن التطبيق")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstAboutApp = aboutAppService.getAll();
            model.oTbAboutApp = new TbAboutApp();

            return View(model);


        }



      
        [HttpPost]
        public async Task<IActionResult> Save(TbAboutApp ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.AboutAppId == null)
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
                    //        ITEM.ab = ImageName;
                    //    }
                    //}





                    var result = aboutAppService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "About App Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in About App Profile  Creating.";
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






                    var result = aboutAppService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "About App Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in About App Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstAboutApp = aboutAppService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف عن التطبيق")]
        public IActionResult Delete(Guid id)
        {

            TbAboutApp oldItem = ctx.TbAboutApps.Where(a => a.AboutAppId == id).FirstOrDefault();



            var result = aboutAppService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "About App Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in About App Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstAboutApp = aboutAppService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل عن التطبيق")]
        public IActionResult Form(Guid? id)
        {
            TbAboutApp oldItem = ctx.TbAboutApps.Where(a => a.AboutAppId == id).FirstOrDefault();
            oldItem = ctx.TbAboutApps.Where(a => a.AboutAppId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
