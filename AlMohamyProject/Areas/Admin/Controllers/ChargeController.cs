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

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChargeController : Controller
    {
        ChargeService chargeService;
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public ChargeController(ChargeService ChargeService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            mainConsultingService = MainConsultingService;
            chargeService = ChargeService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,الشحن")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstCharges = chargeService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbCharge ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.ChargeId == null)
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





                    var result = chargeService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Charge Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Charge Profile  Creating.";
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






                    var result = chargeService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Charge Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Charge Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstCharges = chargeService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الشحن")]
        public IActionResult Delete(Guid id)
        {

            TbCharge oldItem = ctx.TbCharges.Where(a => a.ChargeId == id).FirstOrDefault();



            var result = chargeService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Charge Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Charge Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstCharges = chargeService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل الشحن")]
        public IActionResult Form(Guid? id)
        {
            TbCharge oldItem = ctx.TbCharges.Where(a => a.ChargeId == id).FirstOrDefault();
            oldItem = ctx.TbCharges.Where(a => a.ChargeId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
