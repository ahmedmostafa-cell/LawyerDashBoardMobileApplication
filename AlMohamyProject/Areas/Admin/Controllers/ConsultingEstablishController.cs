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
    public class ConsultingEstablishController : Controller
    {
        SubMainConsultingService subMainConsultingService;
       ServicesService servicesService;
        private readonly UserManager<ApplicationUser> _userManager;
        ConsultingEstablishService consultingEstablishService;
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public ConsultingEstablishController(ServicesService ServicesService,SubMainConsultingService SubMainConsultingService,UserManager<ApplicationUser> userManager, ConsultingEstablishService ConsultingEstablishService,MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            servicesService = ServicesService;
            subMainConsultingService = SubMainConsultingService;
            mainConsultingService = MainConsultingService;
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            _userManager = userManager;

        }
        [Authorize(Roles = "Admin,انشاء الاستشارة")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            ViewBag.lawyers = _userManager.Users.Where(a=> a.UserType == "المحامي").ToList();
            return View(model);


        }
        [Authorize(Roles = "Admin,احصائيات مبيعات ")]
        public IActionResult SalesPerService()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            ViewBag.services = servicesService.getAll();
            return View(model);


        }
        [Authorize(Roles = "Admin,احصائيات مبيعات ")]
        public IActionResult SalesPerLawyer()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View(model);


        }

        
        [Authorize(Roles = "Admin,الاستشارت الغير مكتملة بعدم الرد عل العروض")]
        public IActionResult InCompleteConsultation()
        {

            HomePageModel model = new HomePageModel();
           
            ViewBag.users = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            return View(model);


        }

        [Authorize(Roles = "Admin,الاستشارت الغير مكتملة بالالغاء")]
        public IActionResult CancelledConsultation()
        {

            HomePageModel model = new HomePageModel();

            ViewBag.users = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            return View(model);


        }



        
        public IActionResult LawyrRevenue()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();

            return View(model);


        }

        public IActionResult IndexCustomer()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            ViewBag.Customers = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbConsultingEstablish ITEM, int id, List<IFormFile> files)
        {
           
           
            if (ITEM.ConsultingId == null)
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

                    Random random = new Random();
                    int randomNumber = random.Next(1000, 10000000);
                    ITEM.UserImage = _userManager.Users.Where(a => a.Id == ITEM.UserId).FirstOrDefault().Image;
                    ITEM.MainConsultingName = mainConsultingService.getAll().Where(a => a.MainConsultingId == Guid.Parse(ITEM.MainConsultingId)).FirstOrDefault().MainConsultingTitle;
                    ITEM.SubConsultingName = subMainConsultingService.getAll().Where(a => a.SubMainConsultingId == Guid.Parse(ITEM.SubConsultingId)).FirstOrDefault().SubMainConsultingTitle;
                    ITEM.LawyerName = _userManager.Users.Where(a => a.Id == ITEM.LawyerId).FirstOrDefault().FirstName;
                    ITEM.UserFirstName = _userManager.Users.Where(a => a.Id == ITEM.UserId).FirstOrDefault().FirstName;
                    ITEM.RequestNo = randomNumber.ToString();
                    var result = consultingEstablishService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Consulting Establish Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Consulting Establish Profile  Creating.";
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






                    var result = consultingEstablishService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Consulting Establish Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Consulting Establish Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف انشاء الاستشارة")]
        public IActionResult Delete(Guid id)
        {

            TbConsultingEstablish oldItem = ctx.TbConsultingEstablishes.Where(a => a.ConsultingId == id).FirstOrDefault();



            var result = consultingEstablishService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Consulting Establish Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Consulting Establish Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل انشاء الاستشارة")]
        public IActionResult Form(Guid? id)
        {
            TbConsultingEstablish oldItem = ctx.TbConsultingEstablishes.Where(a => a.ConsultingId == id).FirstOrDefault();
            oldItem = ctx.TbConsultingEstablishes.Where(a => a.ConsultingId == id).FirstOrDefault();

            return View(oldItem);
        }
    }
}
