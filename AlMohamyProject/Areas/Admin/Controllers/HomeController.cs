using AlMohamyProject.Models;
using BL;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Domains;
using AlMohamyProject.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AlMohamyProject.Areas.Admin.Controllers
{
    public class MySearch
    {
        public string id { get; set; }
        public bool OnlyActive { get; set; } = true;
        //public List<string> Ids { get; set; }
    }
    [Area("Admin")]
    [Authorize(Roles = "Admin,الصفحة الرئيسية")]
    public class HomeController : Controller
    {
        private readonly ConsultingEstablishService _consultingEstablishService;
        MainConsultingService mainConsultingService;
        RealTimeNotifcationService realTimeNotifcationService;
        private readonly AlMohamyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public HomeController(MainConsultingService MainConsultingService, IHubContext<OrderHub> orderHub, IHubContext<NotificationHub> notificationHub, AlMohamyDbContext db, UserManager<ApplicationUser> userManager, RealTimeNotifcationService RealTimeNotifcationService, ConsultingEstablishService consultingEstablishService)
        {
            _userManager = userManager;
            mainConsultingService = MainConsultingService;
            realTimeNotifcationService = RealTimeNotifcationService;
            _db = db;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;
            _consultingEstablishService = consultingEstablishService;
        }


        public IActionResult Index()
        {
            List<TbConsultingEstablish> f = _consultingEstablishService.getAll();
            ViewBag.LawyersCount = _userManager.Users.ToList().Where(a => a.UserType == "المحامي").Count();
            ViewBag.UsersCount = _userManager.Users.ToList().Where(a => a.UserType == "المستخدم").Count();
            ViewBag.Admins = _userManager.Users.ToList().Where(a => a.UserType == "ادمن").Count();
            ViewBag.TotaCount = _userManager.Users.ToList().Count();
            ViewBag.ConsultsFinished = _consultingEstablishService.getAll().Where(a => a.RequestStatus == "منتهية").Count();
            ViewBag.ConsultsCurrent = _consultingEstablishService.getAll().Where(a => a.RequestStatus == "حالية").Count();
            ViewBag.ConsultsCancelled = _consultingEstablishService.getAll().Where(a => a.RequestStatus == "ملغية").Count();
            ViewBag.ConsultsWaiting = _consultingEstablishService.getAll().Where(a => a.RequestStatus == "بانتظار الرد").Count();
            return View();
        }
        public IActionResult LawyerConsultIndex()
        {
            ViewBag.LawyersCount = _userManager.Users.ToList().Where(a => a.UserType == "المحامي").Count();

            ViewBag.TotaCount = _userManager.Users.ToList().Count();
            HomePageModel model = new HomePageModel();
            model.lstUsers = _userManager.Users.ToList().Where(a => a.UserType == "المحامي");
            return View(model);
        }
        

        [HttpGet]
       
        public IActionResult LawyerConsult()
        {
            ViewBag.users = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            ViewBag.consults = mainConsultingService.getAll();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult>  LawyerConsult(string id , string nameconsult)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.MainConsultingId = nameconsult;
            user.MainConsultingName = mainConsultingService.getAll().Where(a => a.MainConsultingId == Guid.Parse(nameconsult)).FirstOrDefault().MainConsultingTitle;
            var result = await _userManager.UpdateAsync(user);
            return RedirectToAction("LawyerConsultIndex");
        }
        public IActionResult List()
        {
            ViewBag.LawyersCount = _userManager.Users.ToList().Where(a => a.UserType == "المحامي").Count();
            ViewBag.UsersCount = _userManager.Users.ToList().Where(a => a.UserType == "المستخدم").Count();
            ViewBag.TotaCount = _userManager.Users.ToList().Count();
            HomePageModel model = new HomePageModel();
            model.lstUsers = _userManager.Users.ToList();
            return View(model);
        }
        public IActionResult ListLawyers()
        {
            ViewBag.LawyersCount = _userManager.Users.ToList().Where(a => a.UserType == "المحامي").Count();
          
            ViewBag.TotaCount = _userManager.Users.ToList().Count();
            HomePageModel model = new HomePageModel();
            model.lstUsers = _userManager.Users.ToList().Where(a=> a.UserType == "المحامي");
            return View(model);
        }
      
        public IActionResult DetailsLawyer(string id)
        {
           
            HomePageModel model = new HomePageModel();
            model.OneUser = _userManager.Users.ToList().Where(a => a.Id == id.ToString()).FirstOrDefault();
            return View(model);
        }

        public async Task<IActionResult> LockUnlockAsync(string userId)
        {
            var objFromDb = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (objFromDb == null)
            {
                return NotFound();
            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is locked and will remain locked untill lockoutend time
                //clicking on this action will unlock them
                objFromDb.LockoutEnd = DateTime.Now;
                var user = await _userManager.FindByIdAsync(userId);




                user.Status = "مفعل";










                var r = await _userManager.UpdateAsync(user);
                return RedirectToAction("ListLawyers");
                _db.SaveChanges();
                TempData[SD.Success] = "User unlocked successfully.";
            }
            else if(objFromDb.LockoutEnd == null) 
            {
                objFromDb.LockoutEnd = DateTime.Now;
                var user = await _userManager.FindByIdAsync(userId);




                user.Status = "مفعل";
                var r = await _userManager.UpdateAsync(user);
                _db.SaveChanges();
                TempData[SD.Success] = "User unlocked successfully.";
                return RedirectToAction("ListLawyers");
                
            }
            else
            {
                //user is not locked, and we want to lock the user
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
                var user = await _userManager.FindByIdAsync(userId);




                user.Status = "معلق";










                var r = await _userManager.UpdateAsync(user);
                _db.SaveChanges();
                TempData[SD.Success] = "User locked successfully.";
                return RedirectToAction("ListLawyers");
               
            }
          
          

        }
        public async Task<IActionResult> AcceptLawyerAsync(string id)
        {

            var user = await _userManager.FindByIdAsync(id);




            user.Status = "مفعل";
           









            var r = await _userManager.UpdateAsync(user);
            return RedirectToAction("ListLawyers");
        }





        
        public IActionResult Roles()
        {
            HomePageModel model = new HomePageModel();
            model.lstUserRole = _db.UserRoles.ToList();

            return View(model);
        }
        public IActionResult Account2(string id)
        {
            HomePageModel oHomePageModel = new HomePageModel();
            oHomePageModel.UserData = _db.Users.ToList();
            oHomePageModel.OneUser = _userManager.Users.Where(a => a.Id == id).FirstOrDefault();
            return View(oHomePageModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserAsync(ApplicationUser u, string username, string email, string firstname, List<IFormFile> files)
        {


            var user = await _userManager.FindByEmailAsync(email);

            user.UserName = u.UserName;
            user.Email = u.Email;
            user.FirstName = u.FirstName;
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
                    user.Image = ImageName;
                }
            }







            var result = await _userManager.UpdateAsync(user);


            var a = result;








            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


      
    }
}
