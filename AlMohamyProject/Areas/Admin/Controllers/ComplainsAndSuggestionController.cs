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
    public class ComplainsAndSuggestionController : Controller
    {
        ComplainsAndSuggestionsService complainsAndSuggestionsService;
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public ComplainsAndSuggestionController(ComplainsAndSuggestionsService ComplainsAndSuggestionsService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
           
            mainConsultingService = MainConsultingService;
            complainsAndSuggestionsService = ComplainsAndSuggestionsService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,الشكاوي و الاقتراحات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstComplainsAndSuggestions = complainsAndSuggestionsService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbComplainsAndSuggestion ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.ComplaintsAndSuggestionsId == null)
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





                    var result = complainsAndSuggestionsService.Add(ITEM);
                   


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






                    var result = complainsAndSuggestionsService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Complains And Suggestions Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Complains And Suggestions Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstComplainsAndSuggestions = complainsAndSuggestionsService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الشكاوي و الاقتراحات")]
        public IActionResult Delete(Guid id)
        {

            TbComplainsAndSuggestion oldItem = ctx.TbComplainsAndSuggestions.Where(a => a.ComplaintsAndSuggestionsId == id).FirstOrDefault();



            var result = complainsAndSuggestionsService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Complains And Suggestions Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Complains And Suggestions Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstComplainsAndSuggestions = complainsAndSuggestionsService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل الشكاوي و الاقتراحات")]
        public IActionResult Form(Guid? id)
        {
            TbComplainsAndSuggestion oldItem = ctx.TbComplainsAndSuggestions.Where(a => a.ComplaintsAndSuggestionsId == id).FirstOrDefault();
            oldItem = ctx.TbComplainsAndSuggestions.Where(a => a.ComplaintsAndSuggestionsId == id).FirstOrDefault();

            return View(oldItem);
        }


        public IActionResult Form2(string id)
        {
            TbComplainsAndSuggestion oldItem = ctx.TbComplainsAndSuggestions.Where(a => a.ComplaintsAndSuggestionsId == Guid.Parse(id)).FirstOrDefault();

           
            return View(oldItem);
        }
    }
}
