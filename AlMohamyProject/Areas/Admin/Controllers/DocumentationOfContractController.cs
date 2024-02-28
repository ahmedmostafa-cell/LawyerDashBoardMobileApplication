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
    public class DocumentationOfContractController : Controller
    {
        DocumentationOfContractService documentationOfContractService;
        AlMohamyDbContext ctx;
        public DocumentationOfContractController(DocumentationOfContractService DocumentationOfContractService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            documentationOfContractService = DocumentationOfContractService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,توثيق العقود")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstDocumentationOfContracts = documentationOfContractService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbDocumentationOfContract ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.DocumentationOfContractId == null)
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
                            ITEM.DocumentationOfContractImage = ImageName;
                        }
                    }





                    var result = documentationOfContractService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Documentation Of Contract Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Documentation Of Contract Profile  Creating.";
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
                            ITEM.DocumentationOfContractImage = ImageName;
                        }
                    }






                    var result = documentationOfContractService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Documentation Of Contract Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Documentation Of Contract Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstDocumentationOfContracts = documentationOfContractService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف توثيق العقود")]
        public IActionResult Delete(Guid id)
        {

            TbDocumentationOfContract oldItem = ctx.TbDocumentationOfContracts.Where(a => a.DocumentationOfContractId == id).FirstOrDefault();



            var result = documentationOfContractService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Documentation Of Contract Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Documentation Of Contract Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstDocumentationOfContracts = documentationOfContractService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل توثيق العقود")]
        public IActionResult Form(Guid? id)
        {
            TbDocumentationOfContract oldItem = ctx.TbDocumentationOfContracts.Where(a => a.DocumentationOfContractId == id).FirstOrDefault();
            oldItem = ctx.TbDocumentationOfContracts.Where(a => a.DocumentationOfContractId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
