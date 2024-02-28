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
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        ChatService chatService;
      
        AlMohamyDbContext ctx;
        public ChatController(ChatService ChatService,UserManager<ApplicationUser> userManager, AlMohamyDbContext context)
        {
            chatService = ChatService;
            _userManager = userManager;
            ctx = context;

        }
        [Authorize(Roles = "Admin,الشات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstChats = chatService.getAll();
            ViewBag.lawyers = _userManager.Users.Where(a => a.UserType == "المحامي").ToList();
            ViewBag.users = _userManager.Users.Where(a => a.UserType == "المستخدم").ToList();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbChat ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.ChatId == null)
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





                    var result = chatService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Chat Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Chat Profile  Creating.";
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






                    var result = chatService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Chat Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Chat Profile  Updating.";
                    }
                }
            }




            HomePageModel model = new HomePageModel();
            model.lstChats = chatService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الشات")]
        public IActionResult Delete(Guid id)
        {

            TbChat oldItem = ctx.TbChats.Where(a => a.ChatId == id).FirstOrDefault();



            var result = chatService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Chat Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Chat Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstChats = chatService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل الشات")]
        public IActionResult Form(Guid? id)
        {
            TbChat oldItem = ctx.TbChats.Where(a => a.ChatId == id).FirstOrDefault();
           

            return View(oldItem);
        }
    }
}
