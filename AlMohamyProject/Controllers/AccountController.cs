using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using AlMohamyProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using EmailService;
using Domains;
using System.Web.Helpers;
using BL.Migrations;

namespace AlMohamyProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        LogHistoryService lgHistory;
        private readonly AlMohamyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        //one
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
        private readonly UrlEncoder _urlEncoder;
        private readonly IEmailSender _emailSender;
        public AccountController(LogHistoryService LgHistory, IEmailSender emailSender, AlMohamyDbContext db,  UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            UrlEncoder urlEncoder, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;

            _signInManager = signInManager;
            _urlEncoder = urlEncoder;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _db = db;
            lgHistory = LgHistory;

        }
      


       
      
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnurl = null)
        {
            //two
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                //create roles
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            //three
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "User"
            });



            ViewData["ReturnUrl"] = returnurl;
            //four    five registerview model //six register view
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                RoleList = listItems
            };
            return View(registerViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(FormFileCollection files, RegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.Name,  };
                user.UserType = "ادمن";
                user.DeviceToken = "ادمن";
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //seven
                    if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackurl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    var userEmail = user.Email;

                    var messages = new Message(new string[] { user.Email }, "Email From Customer " + "His name is " + user.UserName + "\n" + " His Email Is " + user.Email + "\n", "This is the content from our async email. i am happy", files, user.Id);

                    await _emailSender.SendEmaillConfirmAsync(messages, code, user.Id, user.Email);


                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return View("EmailConfirmation");
                }
                AddErrors(result);
            }
            //eight
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "User"
            });
            model.RoleList = listItems;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            code = code.Replace(' ', '+');
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                model.RememberMe = true;
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    TbLogHistory item = new TbLogHistory();
                    item.LoggedUserId = Guid.Parse(_userManager.Users.Where(a => a.Email == model.Email).FirstOrDefault().Id);
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedBy = _userManager.Users.Where(a => a.Email == model.Email).FirstOrDefault().Email;
                    item.CreatedBy = _userManager.Users.Where(a => a.Id == item.LoggedUserId.ToString()).FirstOrDefault().UserType;

                    lgHistory.Add(item);

                    ApplicationUser use = _userManager.Users.Where(a => a.Email == model.Email).FirstOrDefault();
                    var claim = await _userManager.GetClaimsAsync(use);
                    if (claim.Count > 0)
                    {
                        try
                        {
                            await _userManager.RemoveClaimAsync(use, claim.FirstOrDefault(u => u.Type == "Name"));
                        }
                        catch (Exception)
                        {

                        }
                    }
                    await _userManager.AddClaimAsync(use, new System.Security.Claims.Claim("Name", use.Email));
                    return LocalRedirect(returnurl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(VerifyAuthenticatorCode), new { returnurl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }


            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model, IFormFileCollection files)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                var messages = new Message(new string[] { user.Email }, "Email From Customer " + "His name is " + user.UserName + "\n" + " His Email Is " + user.Email + "\n", "This is the content from our async email. i am happy", files, user.Id);

                await _emailSender.SendEmailAsync(messages, code, user.Id, user.Email);


                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string UserId, string Code)
        {
            ResetPasswordViewModel resetPasswordModel = new ResetPasswordViewModel
            {
                Code = Code,
                UserId = UserId
            };
            return View(resetPasswordModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                model.Code = model.Code.Replace(' ', '+');
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                AddErrors(result);
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnurl = null)
        {
            //request a redirect to the external login provider
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnurl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return Challenge(properties, provider);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null)
        {
            returnurl = returnurl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Sign in the user with this external login provider, if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true);
            if (result.Succeeded)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                TbLogHistory item = new TbLogHistory();
                item.LoggedUserId = Guid.Parse(_userManager.Users.Where(a => a.Email == email).FirstOrDefault().Id);
                item.CreatedDate = DateTime.Now;
                item.UpdatedBy = email;
                item.CreatedBy = _userManager.Users.Where(a => a.Id == item.LoggedUserId.ToString()).FirstOrDefault().UserType;

                lgHistory.Add(item);

                //update any authentication tokens
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(VerifyAuthenticatorCode), new { returnurl, RememberMe = true });
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    return LocalRedirect(returnurl);

                }

            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("VerifyAuthenticatorCode", new { returnurl = returnurl });
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            FirstName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserType = "ادمن",
                            DeviceToken = "ادمن",

                            Image = "312287690_202877398916203_6801447073936034210_n.jpg"
                        };

                        await _userManager.CreateAsync(user);

                    }
                    user.UserType = "ادمن";
                    user.DeviceToken = "ادمن";
                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TbLogHistory item = new TbLogHistory();
                    item.LoggedUserId = Guid.Parse(_userManager.Users.Where(a => a.Email == email).FirstOrDefault().Id);
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedBy = email;
                    item.CreatedBy = _userManager.Users.Where(a => a.Id == item.LoggedUserId.ToString()).FirstOrDefault().UserType;
                    lgHistory.Add(item);
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction(nameof(VerifyAuthenticatorCode), new { returnurl, RememberMe = true });
                    }
                    if (result.IsLockedOut)
                    {
                        return View("Lockout");
                    }
                    else
                    {
                        return LocalRedirect(returnurl);

                    }

                }

                // If we cannot find the user email we cannot continue

            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("VerifyAuthenticatorCode", new { returnurl = returnurl });
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }

            else
            {
                //If the user does not have account, then we will ask the user to create an account.
                ViewData["ReturnUrl"] = returnurl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email, Name = name });

            }
            ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
            ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

            return View("Error");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnurl = null)
        {
            returnurl = returnurl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                //get the info about the user from external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.Name,  Image = "312287690_202877398916203_6801447073936034210_n.jpg" };
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //nine ten index
                    await _userManager.AddToRoleAsync(user, "User");
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnurl);
                    }
                }
                AddErrors(result);
            }
            ViewData["ReturnUrl"] = returnurl;
            return View(model);
        }









        [HttpGet]
        public async Task<IActionResult> RemoveAuthenticator()
        {

            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            await _userManager.SetTwoFactorEnabledAsync(user, false);
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            var token = await _userManager.GetAuthenticatorKeyAsync(user);
            string AuthenticatorUri = string.Format(AuthenticatorUriFormat, _urlEncoder.Encode("IdentityManager"),
                _urlEncoder.Encode(user.Email), token);
            var model = new TwoFactorAuthenticationViewModel() { Token = token, QRCodeUrl = AuthenticatorUri };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EnableAuthenticator(TwoFactorAuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var succeeded = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);
                if (succeeded == true)
                {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);
                }
                else
                {
                    ModelState.AddModelError("Verify", "Your two factor auth code could not be avalidated.");
                    return View(model);
                }

            }
            return RedirectToAction(nameof(AuthenticatorConfirmation));
        }

        [HttpGet]
        public IActionResult AuthenticatorConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyAuthenticatorCode(bool rememberMe, string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(new VerifyAuthenticatorViewModel { ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorViewModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, rememberClient: false);

            if (result.Succeeded)
            {
                return LocalRedirect(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Code.");
                return View(model);
            }

        }






        public IActionResult Views(string id)
        {
            HomePageModel oHomePageModel = new HomePageModel();
            oHomePageModel.UserData = _db.Users.ToList();
            oHomePageModel.OneUser = _userManager.Users.Where(a => a.Id == id).FirstOrDefault();
            return View(oHomePageModel);
        }






        [HttpGet]
        [Authorize]
        public IActionResult Edit(string id)
        {
            HomePageModel oHomePageModel = new HomePageModel();
            oHomePageModel.UserData = _db.Users.ToList();
            oHomePageModel.OneUser = _userManager.Users.Where(a => a.Id == id).FirstOrDefault();
            return View(oHomePageModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUserAsync(ApplicationUser u, List<IFormFile> files)
        {


            var user = await _userManager.FindByEmailAsync(u.Email);


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
                    user.Image= ImageName;
                }
            }







            var result = await _userManager.UpdateAsync(user);


            if (result.Succeeded)
            {
                TempData[SD.Success] = "User Profile successfully Updated.";
            }
            else
            {
                TempData[SD.Error] = "Error while Updating The Profile";
            }








            return RedirectToAction(nameof(HomeController.Index), "Home");
        }





        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
