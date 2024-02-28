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
    public class CityController : Controller
    {
        CityService cityService;
        AlMohamyDbContext ctx;
        public CityController(CityService CityService, AlMohamyDbContext context)
        {
            cityService = CityService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,المدن")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstCities = cityService.getAll();
           

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbCity ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.CityId == null)
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





                    var result = cityService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "City Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in City Profile  Creating.";
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






                    var result = cityService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "City Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in City Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstCities = cityService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف المدن")]
        public IActionResult Delete(Guid id)
        {

            TbCity oldItem = ctx.TbCities.Where(a => a.CityId == id).FirstOrDefault();



            var result = cityService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "City Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in City Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstCities = cityService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل المدن")]
        public IActionResult Form(Guid? id)
        {
            TbCity oldItem = ctx.TbCities.Where(a => a.CityId == id).FirstOrDefault();
            

            return View(oldItem);
        }
    }
}
