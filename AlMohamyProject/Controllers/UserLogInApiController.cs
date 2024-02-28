using AlMohamyProject.Dtos;
using AlMohamyProject.Helpers;
using AlMohamyProject.Interfaces;
using AlMohamyProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Twilio.Jwt.AccessToken;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    public class mycustomeObject 
    {
        public string result { get; set; }

        public string token { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogInApiController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BaseResponse _baseResponse = new BaseResponse();
        private readonly IAccountService _accountService;
        LogHistoryService lgHistory;
        AlMohamyDbContext db;
        UserManager<ApplicationUser> Usermanager;
        private readonly IAccountRepository _accountRepository;
        public UserLogInApiController(AlMohamyDbContext _db, LogHistoryService LgHistory, UserManager<ApplicationUser> usermanager, IAccountRepository accountRepository, IAccountService accountService, BaseResponse baseResponse, SignInManager<ApplicationUser> signInManager)
        {
            _accountRepository = accountRepository;
            Usermanager = usermanager;
            lgHistory = LgHistory;
            db = _db;
            _accountService = accountService;
            _baseResponse = baseResponse;
            _signInManager = signInManager;
        }

        [HttpPost("forget")]
        public async Task<IActionResult> forget([FromForm] ForgotPasswordViewModel forgetModel)
        {

            var result = await _accountRepository.ForgotPassword(forgetModel);

            if (result == "There is No User By This Name")
            {
                return BadRequest("Enter Phone Number Again");

            };
            if (result == "The Phone number is Wrong")
            {
                return BadRequest("The Phone number is Wrong");
            }
            else if (result == "The Message Has Not Been Sent")
            {
                return BadRequest("The Message Has Not Been Sent");
            }
            mycustomeObject objectt = new mycustomeObject()
            {
                token = result,
                result = "There is Code Sent To Your Phone Number",

            };
            return Ok(objectt);

        }


        [HttpPost("reset-Password")]
        public async Task<IActionResult> resetPassword([FromForm] ForgotPasswordViewModel forgetModel)
        {
            var user = await Usermanager.FindByNameAsync(forgetModel.UserName);
            forgetModel.token = forgetModel.token.Replace(' ', '+');
            var result = await Usermanager.ResetPasswordAsync(await Usermanager.FindByIdAsync(user.Id), forgetModel.token, forgetModel.NewPassword);
            if (result.Succeeded)
            {
                return Ok("The Password Has Been Set");

            }
            else 
            {
                return BadRequest("The Password Has Not Been Set");
            }

          

        }



        [HttpPost("upload-image")]
        public async Task<IActionResult> uploadImage([FromForm] SignUpModel signUpModel)
        {
            var result = await _accountRepository.uploadImage(signUpModel);

            if (result == null)
            {
                return BadRequest("The Images Are Not Saved ");

            }
            else
            {


                return Ok(result);

            }

        }

        [HttpPost("reset-PasswordWithoutToken")]
        public async Task<IActionResult> resetPasswordWithoutToken([FromForm] ForgotPasswordViewModel forgetModel)
        {
            var result = await _accountRepository.ForgotPassword(forgetModel);

            if (result == "There is No User By This Name")
            {
                return BadRequest("Enter Phone Number Again");

            };
            mycustomeObject objectt = new mycustomeObject()
            {
                token = result,
                result = "There is Code Sent To Your Phone Number",

            };
            var user = await Usermanager.FindByNameAsync(forgetModel.UserName);
            objectt.token = objectt.token.Replace(' ', '+');
            var result2 = await Usermanager.ResetPasswordAsync(await Usermanager.FindByIdAsync(user.Id), objectt.token, forgetModel.NewPassword);
            if (result2.Succeeded)
            {
                return Ok("The Password Has Been Set");

            }
            else
            {
                return BadRequest("The Password Has Not Been Set");
            }



        }


        [HttpPost("PhoneCon")]
        public async Task<IActionResult> PhoneCon([FromForm] SignUpModel signUpModel)
        {
            var result = await _accountRepository.pHONEcON(signUpModel);

            if (result == "wrong code")
            {
                return Unauthorized();

            }
            return Ok(result);

        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] SignUpModel signUpModel)
        {
            var result = await _accountRepository.SignUpAsync(signUpModel);

            if (result != "Succeeded")
            {
                return BadRequest(result);

            }
            else
            {
                ApplicationUser user = await Usermanager.FindByNameAsync(signUpModel.UserName);

                return Ok(user);

            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);

            if (result == "Failed" || result == "Lockedout")
            {
                return BadRequest(result);
            }
            else
            {
                ApplicationUser user = await Usermanager.FindByNameAsync(signInModel.UserName);
                TbLogHistory item = new TbLogHistory();
                item.LoggedUserId = Guid.Parse(Usermanager.Users.Where(a => a.Email == user.Email).FirstOrDefault().Id);
                item.CreatedDate = DateTime.Now;
                item.UpdatedBy = user.Email;
                item.CreatedBy = Usermanager.Users.Where(a => a.Id == item.LoggedUserId.ToString()).FirstOrDefault().UserType;
               
                lgHistory.Add(item);
               

                return Ok(user);

            }


        }


        [HttpPost("loginn")]
        public async Task<IActionResult> loginn([FromForm] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);

            if (result == "Failed" || result == "Lockedout")
            {
                return BadRequest(result);
            }
            else
            {
                ApplicationUser user = await Usermanager.FindByNameAsync(signInModel.UserName);
                TbLogHistory item = new TbLogHistory();
                item.LoggedUserId = Guid.Parse(Usermanager.Users.Where(a => a.Email == user.Email).FirstOrDefault().Id);
                item.CreatedDate = DateTime.Now;
                item.UpdatedBy = user.Email;
                item.CreatedBy = Usermanager.Users.Where(a => a.Id == item.LoggedUserId.ToString()).FirstOrDefault().UserType;

                lgHistory.Add(item);


                return Ok(user);

            }


        }
        [HttpPost("bringdata")]
        public  IActionResult bringdata([FromForm] SignInModel signInModel)
        {
            var result =  Usermanager.Users.Where(a=> a.Id == signInModel.Id).FirstOrDefault();

            if (result ==null)
            {
                return BadRequest("There is No User");
            }
            else
            {
               


                return Ok(result);

            }


        }


        [HttpPost("deleteAccount")]
        public async Task<IActionResult> deleteAccount([FromForm] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);

            if (result == "Failed")
            {
                return BadRequest("The Account is not deleted as you are not authirzed");
            }
            else
            {
                var objFromDb = Usermanager.Users.FirstOrDefault(u => u.Id == signInModel.Id);
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
                db.SaveChanges();

                return Ok(new { Result = objFromDb, AdditionalInfo = "The User Is LockedOut" });

            }


        }
        // GET: api/<UserLogInApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserLogInApiController>/5
        [HttpGet("{id}")]
        public ApplicationUser Get(string id)
        {
            return Usermanager.Users.Where(a => a.Id == id).FirstOrDefault();
        }

        // POST api/<UserLogInApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserLogInApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserLogInApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //-------------------------------------------------------------------------------------------- logout Api 
        [HttpPost("logout")]
        
        public async Task<ActionResult<BaseResponse>> LogoutAsync()
        {
         
           
                 await _signInManager.SignOutAsync();
               
           
            
            return Ok("logged Out");
        }


        //[HttpPost("logout")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<ActionResult<BaseResponse>> LogoutAsync([FromHeader] string lang)
        //{
        //    //var userId = this.User.Claims.First(i => i.Type == "uid").Value; // will give the user's userId
        //    var userName = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userName
        //    if (!string.IsNullOrEmpty(userName))
        //    {
        //        var result = await _accountService.Logout(userName);
        //        if (result)
        //        {
        //            _baseResponse.ErrorCode = 0;
        //            _baseResponse.ErrorMessage = (lang == "ar") ? "تم تسجيل الخروج بنجاح " : "Signed out successfully";
        //            return Ok(_baseResponse);
        //        }
        //    }
        //    _baseResponse.ErrorCode = (int)Errors.TheUserNotExistOrDeleted;
        //    _baseResponse.ErrorMessage = (lang == "ar") ? "هذا الحساب غير موجود " : "The User Not Exist";
        //    return Ok(_baseResponse);
        //}



        //[HttpPost("logine")]
        //public async Task<ActionResult<BaseResponse>> LoginAsync([FromBody] LoginModel model, [FromHeader] string lang)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _baseResponse.ErrorCode = (int)Errors.TheModelIsInvalid;
        //        _baseResponse.ErrorMessage = (lang == "ar") ? "خطأ في البيانات" : "Error in data";
        //        _baseResponse.Data = new { message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)) };
        //        return Ok(_baseResponse);
        //    }
        //    var result = await _accountService.LoginAsync(model);

        //    if (!result.IsAuthenticated)
        //    {
        //        _baseResponse.ErrorCode = result.ErrorCode;
        //        _baseResponse.ErrorMessage = (lang == "ar") ? result.ArMessage : result.Message;
        //        _baseResponse.Data = model;
        //        return Ok(_baseResponse);
        //    }
        //    _baseResponse.ErrorCode = 0;
        //    _baseResponse.ErrorMessage = (lang == "ar") ? "تم تسجيل الدخول" : "Login Successfully";
        //    _baseResponse.Data = new
        //    {
        //        result.Email,
        //        result.FullName,
        //        result.Token,
        //        Role = result.Roles,
        //    };
        //    return Ok(_baseResponse);
        //}
    }
}
