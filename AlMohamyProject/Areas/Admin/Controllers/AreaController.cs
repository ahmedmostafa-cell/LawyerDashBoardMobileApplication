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
    public class AreaController : Controller
    {
        CityService cityService;
        AreaService areaService;
        AlMohamyDbContext ctx;
        public AreaController(CityService CityService,AreaService AreaService, AlMohamyDbContext context)
        {
            areaService = AreaService;
             ctx = context;
            cityService = CityService;

        }
        [Authorize(Roles = "Admin,المناطق")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstAreas = areaService.getAll();
            

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbArea ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.AreaId == null)
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





                    var result = areaService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Area Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Area Profile  Creating.";
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






                    var result = areaService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Area Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Area Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstAreas = areaService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف المناطق")]
        public IActionResult Delete(Guid id)
        {

            TbArea oldItem = ctx.TbAreas.Where(a => a.AreaId == id).FirstOrDefault();



            var result = areaService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Area Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Area Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstAreas = areaService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل المناطق")]
        public IActionResult Form(Guid? id)
        {
            TbArea oldItem = ctx.TbAreas.Where(a => a.AreaId == id).FirstOrDefault();

            ViewBag.cities = cityService.getAll();
            return View(oldItem);
        }
    }
}
