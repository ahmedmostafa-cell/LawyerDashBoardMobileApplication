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
    public class SubMainConsultingController : Controller
    {
        MainConsultingService mainConsultingService;
        SubMainConsultingService subMainConsultingService;
        AlMohamyDbContext ctx;
        public SubMainConsultingController(SubMainConsultingService SubMainConsultingService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            subMainConsultingService = SubMainConsultingService;
            ctx = context;
            mainConsultingService = MainConsultingService;

        }
        [Authorize(Roles = "Admin,الاستشارات الفرعية")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstSubMainConsultings = subMainConsultingService.getAll();
            model.lstMainConsultings = mainConsultingService.getAll();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbSubMainConsulting ITEM, int id, List<IFormFile> files)
        {
            ITEM.MainConsultingTitle = mainConsultingService.getAll().Where(a=> a.MainConsultingId == ITEM.MainConsultingId).FirstOrDefault()?.MainConsultingTitle;
            if (ITEM.SubMainConsultingId == null)
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
                            ITEM.SubMainConsultingImage = ImageName;
                        }
                    }





                    var result = subMainConsultingService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Sub Main Consulting Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Sub Main Consulting Profile  Creating.";
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
                            ITEM.SubMainConsultingImage = ImageName;
                        }
                    }






                    var result = subMainConsultingService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Sub Main Consulting Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Sub Main Consulting Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstSubMainConsultings = subMainConsultingService.getAll();
            model.lstMainConsultings = mainConsultingService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الاستشارات الفرعية")]
        public IActionResult Delete(Guid id)
        {

            TbSubMainConsulting oldItem = ctx.TbSubMainConsultings.Where(a => a.SubMainConsultingId == id).FirstOrDefault();



            var result = subMainConsultingService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Sub Main Consulting Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Sub Main Consulting Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstSubMainConsultings = subMainConsultingService.getAll();
            model.lstMainConsultings = mainConsultingService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل الاستشارات الفرعية")]
        public IActionResult Form(Guid? id)
        {
            TbSubMainConsulting oldItem = ctx.TbSubMainConsultings.Where(a => a.SubMainConsultingId == id).FirstOrDefault();
            oldItem = ctx.TbSubMainConsultings.Where(a => a.SubMainConsultingId == id).FirstOrDefault();
            ViewBag.cities = mainConsultingService.getAll();
            return View(oldItem);
        }
    }
}
