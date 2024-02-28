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
    public class PaymentGateController : Controller
    {
        PaymentGateService _paymentGateService;
        AlMohamyDbContext ctx;
        public PaymentGateController(PaymentGateService paymentGateService, AlMohamyDbContext context)
        {
            _paymentGateService = paymentGateService;
            ctx = context;

        }
        [Authorize(Roles = "Admin, بوابات الدفع")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstPaymentGates = _paymentGateService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbPaymentGates ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.PaymentGatesId == null)
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
                            ITEM.PaymentGateImage = ImageName;
                        }
                    }





                    var result = _paymentGateService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Payment Gate Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Payment Gate Profile  Creating.";
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
                            ITEM.PaymentGateImage = ImageName;
                        }
                    }






                    var result = _paymentGateService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Payment Gate Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Payment Gate Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstPaymentGates = _paymentGateService.getAll();
            return RedirectToAction("Index", model);
        }




        [Authorize(Roles = "Admin,حذف بوابات الدفع")]
        public IActionResult Delete(Guid id)
        {

            TbPaymentGates oldItem = ctx.TbPaymentGatess.Where(a => a.PaymentGatesId == id).FirstOrDefault();



            var result = _paymentGateService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Payment Gate Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Payment Gate Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstPaymentGates = _paymentGateService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل بوابات الدفع")]
        public IActionResult Form(Guid? id)
        {
            TbPaymentGates oldItem = ctx.TbPaymentGatess.Where(a => a.PaymentGatesId == id).FirstOrDefault();
           

            return View(oldItem);
        }
    }
}
