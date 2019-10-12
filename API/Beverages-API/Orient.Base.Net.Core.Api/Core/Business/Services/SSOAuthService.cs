using Orient.Base.Net.Core.Api.Core.Business.Models.Users;
using Orient.Base.Net.Core.Api.Core.Common.Utilities;
using Orient.Base.Net.Core.Api.Core.DataAccess.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;
using Orient.Base.Net.Core.Api.Core.Common.Helpers;
using Orient.Base.Net.Core.Api.Core.Common.Constants;
using Orient.Base.Net.Core.Api.Core.Business.Models;
using Microsoft.Extensions.Options;

namespace Orient.Base.Net.Core.Api.Core.Business.Services
{
    public interface ISSOAuthService
    {
        Task<ResponseModel> LoginAsync(UserLoginModel userLoginModel);

        ResponseModel VerifyTokenAsync(string token);
    }

    public class SSOAuthService : ISSOAuthService
    {
        private readonly IUserService _userService;
        private readonly AppSettings _config;

        public SSOAuthService(IUserService userService, IOptions<AppSettings> config)
        {
            _userService = userService;
            _config = config.Value;
        }

        #region Private Methods

        #endregion

        #region Public Methods

        public async Task<ResponseModel> LoginAsync(UserLoginModel userLoginModel)
        {
            var user = await _userService.GetByEmailAsync(userLoginModel.Email);

            if (user != null)
            {
                var result = false;
                var roleIds = user.UserInRoles.Select(x => x.RoleId).ToList();

                result = PasswordUtilities.ValidatePass(user.Password, userLoginModel.Password, user.PasswordSalt);

                var jwtPayload = new JwtPayload()
                {
                    UserId = user.Id,
                    //Mobile = user.Mobile,
                    Name = user.Name,
                    RoleIds = user.UserInRoles != null ? user.UserInRoles.Select(x => x.RoleId).ToList() : null
                };

                var token = JwtHelper.GenerateToken(jwtPayload);

                //result = PasswordUtilities.ValidatePass(user.Password, userLoginModel.Password, user.PasswordSalt);
                if (result)
                {
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = token
                    };
                }

                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Email or Password is incorrect. Please try again!"// TODO: multi language
                };
            }
            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Email or Password is incorrect. Please try again!"// TODO: multi language
            };
        }

        public ResponseModel VerifyTokenAsync(string token)
        {
            var jwtPayload = JwtHelper.ValidateToken(token);

            if (jwtPayload == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Unauthorized request"
                };
            }
            else
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = jwtPayload
                };
            }
        }
    }

    #endregion
}
