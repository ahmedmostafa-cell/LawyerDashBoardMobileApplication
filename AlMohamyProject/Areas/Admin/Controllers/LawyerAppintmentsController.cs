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
using Microsoft.AspNetCore.Identity;

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LawyerAppintmentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        LawyerAppintmentsService lawyerAppintmentsService;
        AlMohamyDbContext ctx;
        public LawyerAppintmentsController(UserManager<ApplicationUser> userManager, LawyerAppintmentsService LawyerAppintmentsService,AboutAppService AboutAppService, MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            lawyerAppintmentsService = LawyerAppintmentsService;
             ctx = context;
            _userManager = userManager;

        }
        [Authorize(Roles = "Admin,مواعيد المحاميين")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstLawyerAppintments = lawyerAppintmentsService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbLawyerAppintments ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.LawyerAppointmentId == null)
            {


                if (ModelState.IsValid)
                {
                    ITEM.LawyerName = _userManager.Users.Where(a => a.Id == ITEM.LawyerId).FirstOrDefault().FirstName;
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





                    var result = lawyerAppintmentsService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Lawyer Appintments Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Lawyer Appintments Profile  Creating.";
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






                    var result = lawyerAppintmentsService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Lawyer Appintments Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Lawyer Appintments Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstLawyerAppintments = lawyerAppintmentsService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف مواعيد المحاميين")]
        public IActionResult Delete(Guid id)
        {

            TbLawyerAppintments oldItem = ctx.TbLawyerAppintmentss.Where(a => a.LawyerAppointmentId == id).FirstOrDefault();



            var result = lawyerAppintmentsService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Lawyer Appintments Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Lawyer Appintments Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstLawyerAppintments = lawyerAppintmentsService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل مواعيد المحاميين")]
        public IActionResult Form(Guid? id)
        {
            TbLawyerAppintments oldItem = ctx.TbLawyerAppintmentss.Where(a => a.LawyerAppointmentId == id).FirstOrDefault();
           

            return View(oldItem);
        }
    }
}
