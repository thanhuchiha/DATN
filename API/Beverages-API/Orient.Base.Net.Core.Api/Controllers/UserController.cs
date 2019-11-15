using Microsoft.AspNetCore.Mvc;
using Orient.Base.Net.Core.Api.Core.Business.Services;
using System;
using Microsoft.AspNetCore.Cors;
using Orient.Base.Net.Core.Api.Core.Business.Models.Users;
using Orient.Base.Net.Core.Api.Core.Business.Filters;
using System.Threading.Tasks;
using Orient.Base.Net.Core.Api.Core.Common.Constants;
using Orient.Base.Net.Core.Api.Core.Business.Models.Base;
using System.Collections.Generic;

namespace Orient.Base.Net.Core.Api.Controllers
{
    [Route("api/users")]
    [EnableCors("CorsPolicy")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(UserRequestListViewModel userRequestListViewModel)
        {
            var users = await _userService.ListUserAsync(userRequestListViewModel);
            return Ok(users);
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUser(BaseRequestGetAllViewModel baseRequestGetAllViewModel)
        {
            var users = await _userService.GetAllUserAsync(baseRequestGetAllViewModel);
            return Ok(users);
        }

        //[HttpGet("hrs")]
        //public async Task<IActionResult> GetAllUserHR(BaseRequestGetAllViewModel baseRequestGetAllViewModel)
        //{
        //    var users = await _userService.GetAllUserHRAsync(baseRequestGetAllViewModel);
        //    return Ok(users);
        //}

        [HttpGet("interviewers")]
        public async Task<IActionResult> GetAllUserInterviewer(BaseRequestGetAllViewModel baseRequestGetAllViewModel)
        {
            var users = await _userService.GetAllUserInterviewerAsync(baseRequestGetAllViewModel);
            return Ok(users);
        }

        [HttpPut("{id}/profile")]
        [CustomAuthorize(Roles = new string[] { RoleConstants.SA })]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserUpdateProfileModel userUpdateProfileModel)
        {
            var responseModel = await _userService.UpdateProfileAsync(id, userUpdateProfileModel);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound("User không tồn tại trong hệ thống. Vui lòng kiểm tra lại!");
            }
            else
            {
                if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(responseModel.Data);
                }
                else
                {
                    return BadRequest(new { Message = responseModel.Message });
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserManageModel userColorManageModel)
        {
            var responseModel = await _userService.UpdateUserAsync(id, userColorManageModel);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }

        [HttpDelete("{id}")]
        [CustomAuthorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var responseModel = await _userService.DeleteUserAsync(id);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel.Data);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }

        #region Other Methods

        [HttpGet("check-existing-email")]
        public async Task<IActionResult> ValidateExistEmail([FromBody] string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            return Ok(user != null);
        }

        #endregion
    }
}
