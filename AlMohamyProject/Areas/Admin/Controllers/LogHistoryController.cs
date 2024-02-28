using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LogHistoryController : Controller
    {

        LogHistoryService logHistoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        AlMohamyDbContext ctx;
        public LogHistoryController(UserManager<ApplicationUser> userManager, LogHistoryService LogHistoryService, AlMohamyDbContext context)
        {
            _userManager = userManager;
            logHistoryService = LogHistoryService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,سجل الدخول")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstLogHistories = logHistoryService.getAll();
            ViewBag.Regions = _userManager.Users.ToList().Where(a=> a.UserType == "المستخدم");
            return View(model);


        }


        [Authorize(Roles = "Admin,سجل الدخول")]
        public IActionResult Index2()
        {

            HomePageModel model = new HomePageModel();
            model.lstLogHistories = logHistoryService.getAll();
            ViewBag.Regions = _userManager.Users.ToList().Where(a => a.UserType == "المحامي");
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbLogHistory ITEM, int id, List<IFormFile> files)
        {
            ITEM.LoggedUserName = _userManager.Users.Where(a => a.Id == ITEM.LoggedUserId.ToString()).FirstOrDefault().FirstName;
            ITEM.CreatedBy = _userManager.Users.Where(a => a.Id == ITEM.LoggedUserId.ToString()).FirstOrDefault().UserType;
            if (ITEM.LogHistoryId == null)
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





                    var result = logHistoryService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "LogHistory Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in LogHistory Profile  Creating.";
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






                    var result = logHistoryService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "LogHistory Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in LogHistory Profile  Updating.";
                    }
                }
            }




            HomePageModel model = new HomePageModel();
            model.lstLogHistories = logHistoryService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف سجل الدخول")]
        public IActionResult Delete(Guid id)
        {

            TbLogHistory oldItem = ctx.TbLogHistories.Where(a => a.LogHistoryId == id).FirstOrDefault();



            var result = logHistoryService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "LogHistory Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in LogHistory Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstLogHistories = logHistoryService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل سجل الدخول")]
        public IActionResult Form(Guid? id)
        {
            TbLogHistory oldItem = ctx.TbLogHistories.Where(a => a.LogHistoryId == id).FirstOrDefault();
            oldItem = ctx.TbLogHistories.Where(a => a.LogHistoryId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
