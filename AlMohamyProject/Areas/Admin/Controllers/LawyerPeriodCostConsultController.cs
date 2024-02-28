using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class LawyerPeriodCostConsultController : Controller
    {

        SubMainConsultingService subMainConsultingService;
        LawyerPeriodCostConsultService lawyerPeriodCostConsultService;
        private readonly UserManager<ApplicationUser> _userManager;
        ConsultingEstablishService consultingEstablishService;
        MainConsultingService mainConsultingService;
        AlMohamyDbContext ctx;
        public LawyerPeriodCostConsultController(LawyerPeriodCostConsultService LawyerPeriodCostConsultService,SubMainConsultingService SubMainConsultingService, UserManager<ApplicationUser> userManager, ConsultingEstablishService ConsultingEstablishService, MainConsultingService MainConsultingService, AlMohamyDbContext context)
        {
            subMainConsultingService = SubMainConsultingService;
            mainConsultingService = MainConsultingService;
            consultingEstablishService = ConsultingEstablishService;
            ctx = context;
            lawyerPeriodCostConsultService = LawyerPeriodCostConsultService;
            _userManager = userManager;

        }
        [Authorize(Roles = "Admin,اضافة تكلفة الاستشارة بالمدة للمحامي")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            model.lstLawyerPeriodCostConsults = lawyerPeriodCostConsultService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View(model);


        }


        public IActionResult LawyrRevenue()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            model.lstLawyerPeriodCostConsults = lawyerPeriodCostConsultService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();

            return View(model);


        }

        public IActionResult IndexCustomer()
        {

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            model.lstLawyerPeriodCostConsults = lawyerPeriodCostConsultService.getAll();
            ViewBag.Customers = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbLawyerPeriodCostConsult ITEM, int id, List<IFormFile> files)
        {


            if (ITEM.LawyerPeriodCostConsultId == null)
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
                    ITEM.LawyerName = _userManager.Users.Where(a => a.Id == ITEM.LawyerId).FirstOrDefault().FirstName;
                 
                    var result = lawyerPeriodCostConsultService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Lawyer Consult Cost Period Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Lawyer Consult Cost Period Profile  Creating.";
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






                    var result = lawyerPeriodCostConsultService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Lawyer Consult Cost Period Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Lawyer Consult Cost Period Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            model.lstLawyerPeriodCostConsults = lawyerPeriodCostConsultService.getAll();

            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف تكلفة الاستشارة بالمدة للمحامي")]
        public IActionResult Delete(Guid id)
        {

            TbLawyerPeriodCostConsult oldItem = ctx.TbLawyerPeriodCostConsults.Where(a => a.LawyerPeriodCostConsultId == id).FirstOrDefault();



            var result = lawyerPeriodCostConsultService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Lawyer Consult Cost Period Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Lawyer Consult Cost Period Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstConsultingEstablishs = consultingEstablishService.getAll();
            model.lstApprovedOffices = ctx.TbApprovedOffices.ToList();
            model.lstLawyerPeriodCostConsults = lawyerPeriodCostConsultService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل تكلفة الاستشارة بالمدة للمحامي")]
        public IActionResult Form(Guid? id)
        {
            TbLawyerPeriodCostConsult oldItem = ctx.TbLawyerPeriodCostConsults.Where(a => a.LawyerPeriodCostConsultId == id).FirstOrDefault();
            oldItem = ctx.TbLawyerPeriodCostConsults.Where(a => a.LawyerPeriodCostConsultId == id).FirstOrDefault();
            ViewBag.userss = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            return View(oldItem);
        }
    }
}
