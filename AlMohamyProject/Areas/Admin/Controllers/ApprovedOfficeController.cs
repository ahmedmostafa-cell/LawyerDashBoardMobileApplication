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
    public class ApprovedOfficeController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        RealTimeNotifcationService realTimeNotifcationService;
        ApprovedOfficeService approvedOfficeService;
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public ApprovedOfficeController(RealTimeNotifcationService RealTimeNotifcationService, ApprovedOfficeService ApprovedOfficeService, MainConsultingService MainConsultingService, AlMohamyDbContext context, UserManager<ApplicationUser> usermanager)
        {
            mainConsultingService = MainConsultingService;
            approvedOfficeService = ApprovedOfficeService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _usermanager = usermanager;
        }
        [Authorize(Roles = "Admin,المكاتب المعتمدة")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstApprovedOffices = approvedOfficeService.getAll();

            return View(model);


        }
      
        public IActionResult RealTimeNotificationnnnnnnnnnnnnnnnnnnn(MySearch arr)
        {



            TbRealTimeNotifcation oTbRealTimeNotifcation = ctx.TbRealTimeNotifcations.Where(a => a.RealTimeNotifcationId == Guid.Parse(arr.id)).FirstOrDefault();
            if (oTbRealTimeNotifcation.UpdatedBy == "اضافة استفسار")
            {
                Guid? id = Guid.Parse(oTbRealTimeNotifcation.CreatedBy);
                var data = new { id = id, type = oTbRealTimeNotifcation.UpdatedBy };
                realTimeNotifcationService.Delete(oTbRealTimeNotifcation);
                //return RedirectToAction("Form", "ContactUs", new { id });

                return Json(data);
            }
            else
            {
                Guid id = Guid.Parse(oTbRealTimeNotifcation.CreatedBy);
                var data = new { id = id, type = oTbRealTimeNotifcation.UpdatedBy };
                realTimeNotifcationService.Delete(oTbRealTimeNotifcation);
                //return RedirectToAction("Form", "Complains", new { id });

                return Json(data);
            }


        }

      

        [HttpPost]
        public async Task<IActionResult> Save(TbApprovedOffice ITEM, int id, List<IFormFile> files, List<IFormFile> filess)
        {
            if (ITEM.ApprovedOfficeId == null)
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
                            ITEM.ApprovedOfficeLogo = ImageName;
                        }
                    }

                    foreach (var file in filess)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".pdf";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.ApprovedOfficeLicenseDoc = ImageName;
                        }
                    }






                    var result = approvedOfficeService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Approved Office Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Approved Office Profile  Creating.";
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
                            ITEM.ApprovedOfficeLogo = ImageName;
                        }
                    }



                    foreach (var file in filess)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".pdf";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.ApprovedOfficeLicenseDoc = ImageName;
                        }
                    }


                    var result = approvedOfficeService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Approved Office Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Approved Office Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstApprovedOffices = approvedOfficeService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف المكاتب المعتمدة")]
        public IActionResult Delete(Guid id)
        {

            TbApprovedOffice oldItem = ctx.TbApprovedOffices.Where(a => a.ApprovedOfficeId == id).FirstOrDefault();



            var result = approvedOfficeService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Approved Office Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Approved Office Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstApprovedOffices = approvedOfficeService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل المكاتب المعتمدة")]
        public IActionResult Form(Guid? id)
        {
            TbApprovedOffice oldItem = ctx.TbApprovedOffices.Where(a => a.ApprovedOfficeId == id).FirstOrDefault();
            oldItem = ctx.TbApprovedOffices.Where(a => a.ApprovedOfficeId == id).FirstOrDefault();

            return View(oldItem);
        }

        public IActionResult Form2(string id)
        {
            TbApprovedOffice oldItem = ctx.TbApprovedOffices.Where(a => a.ApprovedOfficeId == Guid.Parse(id)).FirstOrDefault();


            return View(oldItem);
        }
        [Authorize(Roles = "Admin,اضافة او تعديل المكاتب المعتمدة")]
        public async Task<IActionResult> ApproveOfficeAsync(Guid? id)
        {
            TbApprovedOffice oldItem = ctx.TbApprovedOffices.Where(a => a.ApprovedOfficeId == id).FirstOrDefault();
            if(oldItem.ApprovalStatus == "مفعل") 
            {
                oldItem.ApprovalStatus = "غير مفعل";
            }
            else if(oldItem.ApprovalStatus == "غير مفعل") 
            {
                oldItem.ApprovalStatus = "مفعل";
            }
            
            var user = _usermanager.Users.Where(a => a.Id == oldItem.CreatedBy).FirstOrDefault();
            if (user != null) 
            {
                var result = approvedOfficeService.Edit(oldItem);
                if (result == true)
                {
                    if(user.IsApprovedOffice == false) 
                    {
                        user.IsApprovedOffice = true;
                    }
                    else if(user.IsApprovedOffice == true) 
                    {
                        user.IsApprovedOffice = false;
                    }
                   
                    var result2 =await _usermanager.UpdateAsync(user);
                    if(result2.Succeeded ==true) 
                    {
                        TempData[SD.Success] = "Approved Office Profile successfully Removed.";
                    }
                   
                }
                else
                {
                    TempData[SD.Error] = "Error in Approved Office Profile  Removing.";
                }

               

            }
            HomePageModel model = new HomePageModel();
            model.lstApprovedOffices = approvedOfficeService.getAll();
            return View("Index" , model);
        }




        
    }
}
