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
    public class NotificationController : Controller
    {
        NotificationService notificationService;
        AlMohamyDbContext ctx;
        UserManager<ApplicationUser> Usermanager;
        public NotificationController(UserManager<ApplicationUser> usermanager, NotificationService NotificationService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            notificationService = NotificationService;
            ctx = context;
            Usermanager = usermanager;
        }
        [Authorize(Roles = "Admin,الاشعارات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstNotifications = notificationService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbNotification ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.NotificationId == null)
            {
                ITEM.ToWhomName = Usermanager.Users.Where(a=> a.Id == ITEM.ToWhomId).FirstOrDefault().FirstName;

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





                    var result = notificationService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Notification Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Notification Profile  Creating.";
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






                    var result = notificationService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Notification Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Notification Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstNotifications = notificationService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الاشعارات")]
        public IActionResult Delete(Guid id)
        {

            TbNotification oldItem = ctx.TbNotifications.Where(a => a.NotificationId == id).FirstOrDefault();



            var result = notificationService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Notification Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Notification Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstNotifications = notificationService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل الاشعارات")]
        public IActionResult Form(Guid? id)
        {
            TbNotification oldItem = ctx.TbNotifications.Where(a => a.NotificationId == id).FirstOrDefault();
            oldItem = ctx.TbNotifications.Where(a => a.NotificationId == id).FirstOrDefault();
            ViewBag.cities = Usermanager.Users.ToList();
            return View(oldItem);
        }
    }
}
