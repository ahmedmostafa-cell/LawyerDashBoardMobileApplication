using AlMohamyProject.Dtos;
using BL.Interfaces;
using BL;
using Microsoft.AspNetCore.Mvc;
using AlMohamyProject.Helpers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AlMohamyProject.Controllers
{
    public class DeactivateLawyerController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BaseResponse _baseResponse;
        private ApplicationUser _user;
        public DeactivateLawyerController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _baseResponse = new BaseResponse();
            _userManager = userManager;
        }
        //---------------------------------------------------------------------------------------------------

        [HttpPost("deactivateAccount")]
        public async Task<ActionResult<BaseResponse>> deactivateAccount([FromForm] string id)
        {
            var user = await _userManager.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                _baseResponse.ErrorCode = (int)Errors.TheUserNotExistOrDeleted;
                _baseResponse.ErrorMessage ="هذا الحساب غير موجود ";
                   
                return Ok(_baseResponse);
            }


            user.IsActivated = false;
            
            await _userManager.UpdateAsync(user);

            _baseResponse.ErrorCode = 0;
            _baseResponse.ErrorMessage =  "تم تعليق الاكونت بنجاح" ;
            return Ok(_baseResponse);

        }
    }
}
