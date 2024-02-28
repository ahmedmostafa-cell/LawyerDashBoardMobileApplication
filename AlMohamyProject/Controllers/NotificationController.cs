using AlMohamyProject.Dtos;
using AlMohamyProject.Helpers;
using BL;
using BL.Interfaces;
using Domains;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AlMohamyProject.Controllers
{
    public class NotificationController : BaseApiController
    {
       
        private readonly BaseResponse _baseResponse;
       
       
        private readonly UserManager<ApplicationUser> _userMananger;
        private readonly IAccountRepository _accountRepository;
        public NotificationController( UserManager<ApplicationUser> userMananger, IAccountRepository accountRepository)
        {
            
           
            _baseResponse = new BaseResponse();
          
            _userMananger = userMananger;
            _accountRepository = accountRepository;
        }
        [HttpPost("updateUserToken")]
        public async Task<IActionResult> SendNotification(NotificationDto notificationDto)
        {
            if (!ModelState.IsValid)
            {
                //_baseResponse.ErrorMessage = (lang == "ar") ? "خطأ في البيانات" : "Error in data";
                _baseResponse.ErrorCode = (int)Errors.TheModelIsInvalid;
                _baseResponse.Data = new { message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)) };
                return BadRequest(_baseResponse);
            }
            var user = _userMananger.Users.Where(a => a.Id == notificationDto.userId).FirstOrDefault(); // will give the user data 
            user.DeviceToken = notificationDto.Token;
            var result = await _accountRepository.UpdateUserToken(user);
          
            if (result == null)
            {
                _baseResponse.ErrorCode = (int)Errors.TheUserNotExistOrDeleted;
                //_baseResponse.ErrorMessage = (lang == "ar") ? "المستخدم غير موجود " : "The User Not Exist Or Deleted";
                _baseResponse.Data = null;
                return BadRequest(_baseResponse);
            }

            _baseResponse.ErrorCode = (int)Errors.Success;
          
            _baseResponse.Data = new
            {
                user.Email,
                user.FirstName,
                user.PhoneNumber,

                user.UserType,


                user.DeviceToken,
            };
            return Ok(_baseResponse);



           
           
        }
    }
}
