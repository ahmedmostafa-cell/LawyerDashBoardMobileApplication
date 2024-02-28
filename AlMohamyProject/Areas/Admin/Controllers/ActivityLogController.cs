using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Http;
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
    public class ActivityLogController : Controller
    {
        ActivityLogService _activityLogService;
        AlMohamyDbContext ctx;
        public ActivityLogController(ActivityLogService activityLogService, AlMohamyDbContext context)
        {
            _activityLogService = activityLogService;
           
            ctx = context;

        }
        [Authorize(Roles = "Admin, عدد الزوار الحاليين")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstActivityLogs = _activityLogService.getAll();
           

            return View(model);


        }

        [Authorize(Roles = "Admin, عدد الزوار الحاليين")]
        public IActionResult Index2()
        {

            HomePageModel model = new HomePageModel();
            model.lstActivityLogs = _activityLogService.getAll();


            return View(model);


        }
        [Authorize(Roles = "Admin, معدل الزيارات خلال الاسبوع الحالي و الاسبوع الماضي")]
        public IActionResult Index3()
        {

            HomePageModel model = new HomePageModel();
            model.lstActivityLogs = _activityLogService.getAll();


            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbActivityLog ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.Id == null)
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





                    var result = _activityLogService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Activity Log  Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Activity Log  Profile  Creating.";
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






                    var result = _activityLogService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Activity Log  Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Activity Log  Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstActivityLogs = _activityLogService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف  عدد الزوار الحاليين")]
        public IActionResult Delete(Guid id)
        {

            TbActivityLog oldItem = ctx.TbActivityLogs.Where(a => a.Id == id).FirstOrDefault();



            var result = _activityLogService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Activity Log  Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Activity Log  Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstActivityLogs = _activityLogService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل  عدد الزوار الحاليين")]
        public IActionResult Form(Guid? id)
        {
            TbActivityLog oldItem = ctx.TbActivityLogs.Where(a => a.Id == id).FirstOrDefault();
           

            return View(oldItem);
        }
    }
}
