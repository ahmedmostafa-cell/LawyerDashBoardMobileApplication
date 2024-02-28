using AlMohamyProject.Models;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Domains;
using System.Linq;

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainConsultingController : Controller
    {
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public MainConsultingController(MainConsultingService MainConsultingService ,AlMohamyDbContext context)
        {
            mainConsultingService = MainConsultingService;
            ctx = context;
            
        }
        [Authorize(Roles = "Admin,الاستشارات الرئيسية")]
        public IActionResult Index()
        {
           
            HomePageModel model = new HomePageModel();
            model.lstMainConsultings = mainConsultingService.getAll();
           
            return View(model);


        }



       
        [HttpPost]
        public async Task<IActionResult> Save(TbMainConsulting ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.MainConsultingId == null)
            {

               
                if (ModelState.IsValid)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".jpg";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.MainConsultingImage = ImageName;
                        }
                    }





                    var result = mainConsultingService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Main Consulting Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Main Consulting Profile  Creating.";
                    }


                }
              

            }
            else
            {
                if (ModelState.IsValid) 
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".jpg";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.MainConsultingImage = ImageName;
                        }
                    }






                    var result = mainConsultingService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Main Consulting Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Main Consulting Profile  Updating.";
                    }

                }


                   

            }




            HomePageModel model = new HomePageModel();
            model.lstMainConsultings = mainConsultingService.getAll();
            return RedirectToAction("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الاستشارات الرئيسية")]
        public IActionResult Delete(Guid id)
        {
           
            TbMainConsulting oldItem = ctx.TbMainConsultings.Where(a => a.MainConsultingId == id).FirstOrDefault();

           

            var result = mainConsultingService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Main Consulting Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Main Consulting Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstMainConsultings = mainConsultingService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل الاستشارات الرئيسية")]
        public IActionResult Form(Guid? id)
        {
            TbMainConsulting oldItem = ctx.TbMainConsultings.Where(a => a.MainConsultingId == id).FirstOrDefault();
            oldItem = ctx.TbMainConsultings.Where(a => a.MainConsultingId == id).FirstOrDefault();
          
            return View(oldItem);
        }
    }
}
