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
    public class ConsultingTypeController : Controller
    {
        ConsultingTypeService consultingTypeService;
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public ConsultingTypeController(ConsultingTypeService ConsultingTypeService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            mainConsultingService = MainConsultingService;
            consultingTypeService = ConsultingTypeService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,نوع الاستشارة محدد ام لا")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingTypes = consultingTypeService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbConsultingType ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.ConsultingTypeId == null)
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





                    var result = consultingTypeService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Consulting Type Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Consulting Type Profile  Creating.";
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






                    var result = consultingTypeService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Consulting Type Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Consulting Type Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstConsultingTypes = consultingTypeService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف نوع الاستشارة محدد ام لا")]
        public IActionResult Delete(Guid id)
        {

            TbConsultingType oldItem = ctx.TbConsultingTypes.Where(a => a.ConsultingTypeId == id).FirstOrDefault();



            var result = consultingTypeService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Consulting Type Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Consulting Type Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstConsultingTypes = consultingTypeService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل نوع الاستشارة محدد ام لا")]
        public IActionResult Form(Guid? id)
        {
            TbConsultingType oldItem = ctx.TbConsultingTypes.Where(a => a.ConsultingTypeId == id).FirstOrDefault();
            oldItem = ctx.TbConsultingTypes.Where(a => a.ConsultingTypeId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
