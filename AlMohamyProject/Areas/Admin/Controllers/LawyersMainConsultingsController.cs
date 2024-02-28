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
    public class LawyersMainConsultingsController : Controller
    {
        private readonly MainConsultingService mainConsultingService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LawyersMainConsultingsService _lawyersMainConsultingsService;
        AlMohamyDbContext ctx;
        public LawyersMainConsultingsController(MainConsultingService MainConsultingService,UserManager<ApplicationUser> userManager, AlMohamyDbContext context, LawyersMainConsultingsService lawyersMainConsultingsService )
        {

            ctx = context;
            _lawyersMainConsultingsService = lawyersMainConsultingsService;
            _userManager = userManager;
            mainConsultingService = MainConsultingService;

        }
        [Authorize(Roles = "Admin,ربط الاستشارات بالمحاميين")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstLawyersMainConsultings = _lawyersMainConsultingsService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            ViewBag.mainconsultings = mainConsultingService.getAll();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbLawyersMainConsultings ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.MainConsultingId == null)
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





                    var result = _lawyersMainConsultingsService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Lawyers Main Consulting Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Lawyers Main Consulting Profile  Creating.";
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






                    var result = _lawyersMainConsultingsService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Lawyers Main Consulting Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Lawyers Main Consulting Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstLawyersMainConsultings = _lawyersMainConsultingsService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            ViewBag.mainconsultings = mainConsultingService.getAll();
            return RedirectToAction("Index", model);
        }




        [Authorize(Roles = "Admin,حذف ربط الاستشارات بالمحاميين")]
        public IActionResult Delete(Guid id)
        {

            TbLawyersMainConsultings oldItem = ctx.TbLawyersMainConsultingss.Where(a => a.LawyersMainConsultingsId == id).FirstOrDefault();



            var result = _lawyersMainConsultingsService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Main Consulting Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Main Consulting Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstLawyersMainConsultings = _lawyersMainConsultingsService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            ViewBag.mainconsultings = mainConsultingService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل ربط الاستشارات بالمحاميين")]
        public IActionResult Form(Guid? id)
        {
            TbLawyersMainConsultings oldItem = ctx.TbLawyersMainConsultingss.Where(a => a.LawyersMainConsultingsId == id).FirstOrDefault();

            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            ViewBag.mainconsultings = mainConsultingService.getAll();
            return View(oldItem);
        }


        [Authorize(Roles = "Admin,اضافة او تعديل ربط الاستشارات بالمحاميين")]
        public async Task<IActionResult> ApproveAsync(Guid? id)
        {
            TbLawyersMainConsultings oldItem = ctx.TbLawyersMainConsultingss.Where(a => a.LawyersMainConsultingsId == id).FirstOrDefault();
            if(oldItem.Status == "مفعل") 
            {
                oldItem.Status = "غير مفعل";
            }
            else 
            {
                oldItem.Status = "مفعل";
            }
          
            var result = _lawyersMainConsultingsService.Edit(oldItem);
            if(result == true) 
            {
               
                        TempData[SD.Success] = "Lawyers Main Consulting Profile successfully Updated.";
                   
                       
                    
                
            }
            else 
            {
                TempData[SD.Error] = "Error in Lawyers Main Consulting Profile  Updating.";
            }
           
           
           
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            ViewBag.mainconsultings = mainConsultingService.getAll();
            HomePageModel model = new HomePageModel();
            model.lstLawyersMainConsultings = _lawyersMainConsultingsService.getAll();
            return View("Index", model);
        }
    }
}
