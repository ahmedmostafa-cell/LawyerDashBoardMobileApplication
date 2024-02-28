using AlMohamyProject.Dtos;
using BL.Interfaces;
using BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AlMohamyProject.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AlMohamyProject.Controllers
{
    public class ActivateLawyerApiController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BaseResponse _baseResponse;
        private ApplicationUser _user;
        public ActivateLawyerApiController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _baseResponse = new BaseResponse();
            _userManager = userManager;
        }
        //---------------------------------------------------------------------------------------------------
        [HttpPost("actvateAccount")]
        public async Task<ActionResult<BaseResponse>> actvateAccount([FromForm] string id)
        {
            var user = await _userManager.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                _baseResponse.ErrorCode = (int)Errors.TheUserNotExistOrDeleted;
                _baseResponse.ErrorMessage = "هذا الحساب غير موجود ";

                return Ok(_baseResponse);
            }


            user.IsActivated = true;

            await _userManager.UpdateAsync(user);

            _baseResponse.ErrorCode = 0;
            _baseResponse.ErrorMessage = "تم تفعيل الاكونت بنجاح";
            return Ok(_baseResponse);

        }
    }
}
