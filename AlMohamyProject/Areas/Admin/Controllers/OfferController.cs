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
    public class OfferController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        OfferService offerService;
        AlMohamyDbContext ctx;
        public OfferController(UserManager<ApplicationUser> userManager, OfferService OfferService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            offerService = OfferService;
            ctx = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin,العروض")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbOffer ITEM, int id, List<IFormFile> files)
        {
         
            if (ITEM.OfferId == null)
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





                    var result = offerService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Offer Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Offer Profile  Creating.";
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






                    var result = offerService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Offer Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Offer Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف العروض")]
        public IActionResult Delete(Guid id)
        {

            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == id).FirstOrDefault();



            var result = offerService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Offer Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Offer Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل العروض")]
        public IActionResult Form(Guid? id)
        {
            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == id).FirstOrDefault();
            oldItem = ctx.TbOffers.Where(a => a.OfferId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
