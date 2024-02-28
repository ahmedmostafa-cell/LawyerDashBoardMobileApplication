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
    public class ServiceController : Controller
    {
        ServicesService servicesService;
        AlMohamyDbContext ctx;
        public ServiceController(ServicesService ServicesService, AlMohamyDbContext context)
        {
            servicesService = ServicesService;
            ctx = context;
            

        }
        [Authorize(Roles = "Admin,الخدمات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstServices = servicesService.getAll();


            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbServices ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.ServiceId == null)
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





                    var result = servicesService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Service Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Service Profile  Creating.";
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






                    var result = servicesService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Service Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Service Profile  Updating.";
                    }
                }
            }




            HomePageModel model = new HomePageModel();
            model.lstServices = servicesService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الخدمات")]
        public IActionResult Delete(Guid id)
        {

            TbServices oldItem = ctx.TbServicess.Where(a => a.ServiceId == id).FirstOrDefault();



            var result = servicesService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Service Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Service Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstServices = servicesService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل الخدمات")]
        public IActionResult Form(Guid? id)
        {
            TbServices oldItem = ctx.TbServicess.Where(a => a.ServiceId == id).FirstOrDefault();

           
            return View(oldItem);
        }
    }
}
