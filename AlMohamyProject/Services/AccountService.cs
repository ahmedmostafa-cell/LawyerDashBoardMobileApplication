using AlMohamyProject.Helpers;
using AlMohamyProject.Interfaces;
using AlMohamyProject.Models;
using BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlMohamyProject.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Jwt _jwt;
        private readonly AlMohamyDbContext ctx;

        public AccountService(UserManager<ApplicationUser> userManager,
            AlMohamyDbContext context ,
            IOptions<Jwt> jwt)
        {
            _userManager = userManager;
            ctx = context;
            _jwt = jwt.Value;
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return new AuthModel { Message = "Your phone number is not Exist!", ArMessage = "البريد الالكتروني غير مسجل", ErrorCode = (int)Errors.thisEmailNotExist };
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthModel { Message = "Password is not correct!", ArMessage = "كلمة المرور غير صحيحة", ErrorCode = (int)Errors.TheUsernameOrPasswordIsIncorrect };
            //if (!user.Status)
            //    return new AuthModel { Message = "Your account has been suspended!", ArMessage = "حسابك تم إيقافة", ErrorCode = (int)Errors.UserIsBloked };


            var rolesList = _userManager.GetRolesAsync(user).Result.ToList();
            user.DeviceToken = model.DeviceToken;
            await _userManager.UpdateAsync(user);

            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FirstName,
                IsAuthenticated = true,
                Roles = rolesList,
                //IsAdmin = user.IsAdmin,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

        }

        //-------------------------------------------------------------------------------------------------------------------------

        public async Task<bool> Logout(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return false;

            user.DeviceToken = null;
            await _userManager.UpdateAsync(user);
            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------

       

        #region create and validate JWT token

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user, int? time = null)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("Name", user.FirstName),
                
            }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: (time != null) ? DateTime.Now.AddHours((double)time) : DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


        public string ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == "uid").Value;

                return accountId;
            }
            catch
            {
                return null;
            }
        }

        #endregion create and validate JWT token
        //------------------------------------------------------------------------------------------------------------
    }
}
